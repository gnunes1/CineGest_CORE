using cinegest.Areas.Identity.Pages.Account.Manage;
using cinegest.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace cinegest.Controllers
{
    public class UsersController : Controller
    {
        private readonly CinegestDB _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<DeletePersonalDataModel> _logger;
        public static IWebHostEnvironment _environment;

        public UsersController(CinegestDB context,
            SignInManager<ApplicationUser> signInManager,
            ILogger<DeletePersonalDataModel> logger,
            IWebHostEnvironment environment,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _environment = environment;
        }

        // GET: Users
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var applicationUser = await _userManager.GetUserAsync(User);


            return View(await _context.User.Where(u => u.Id != applicationUser.User).ToListAsync());
        }

        // GET: Users/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // GET: Users/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            List<SelectListItem> list = new List<SelectListItem> {
                new SelectListItem() { Text = "User", Value = "User" },
                new SelectListItem() { Text = "Admin", Value = "Admin" }
            };

            ViewData["Roles"] = new SelectList(list, "Value", "Text");
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int Id, string Email, string Name, DateTime DoB, string Role, IFormFile Avatar)
        {
            var user = _context.User.Find(Id);
            if (user == null) return NotFound();

            if (ModelState.IsValid)
            {
                if (_context.User.Where(u => u.Email == Email && u.Id != Id).Any())//verifica se existe algum utilizador com o mesmo email
                {
                    ViewData["message"] = "Já existe um utilizador com o mesmo email.";
                    return View(_context.User.Find(Id));
                }

                try
                {
                    //verifica se o avatar mudou
                    if (Avatar == null)
                    { } //não muda a imagem
                    else if (!Avatar.ContentType.Contains("image")) //se o poster não fôr imagem usa-se a imagem default
                    {
                        //apaga a antiga imagem
                        if (user.Avatar != "default.png") System.IO.File.Delete(_environment.WebRootPath + "/images/users/" + user.Avatar);

                        //guarda a nova imagem
                        user.Avatar = "default.png";
                    }
                    else
                    { //update atualiza a imagem

                        //apaga o antigo poster excepto se for o poster default
                        if (user.Avatar != "default.png") System.IO.File.Delete(_environment.WebRootPath + "/images/users/" + user.Avatar);

                        Guid g;
                        g = Guid.NewGuid();

                        string extensao = Path.GetExtension(Avatar.FileName).ToLower();

                        // caminho do ficheiro 
                        user.Avatar = g.ToString() + extensao;

                        //guarda a imagem
                        using var fileStream = new FileStream(_environment.WebRootPath + "/images/users/" + g.ToString() + extensao, FileMode.Create);
                        await Avatar.CopyToAsync(fileStream);
                    }
                    user.Email = Email;
                    user.Name = Name;
                    user.DoB = DoB;
                    user.Role = Role;
                    _context.Update(user);

                    var netUser = await _context.Users.Where(u => u.User == Id).FirstOrDefaultAsync();
                    netUser.UserName = Email;
                    netUser.Email = Email;
                    netUser.Nome = Name;

                    var userRole = await _context.UserRoles.Where(uR => uR.UserId == netUser.Id).FirstOrDefaultAsync();
                    if (userRole.RoleId == "1" && Role == "User") //remove Admin e dá User
                    {
                        await _userManager.RemoveFromRoleAsync(netUser, "Admin");
                        await _userManager.AddToRoleAsync(netUser, "User");
                    }
                    else if (userRole.RoleId == "2" && Role == "Admin") // remove User e dá Admin
                    {
                        await _userManager.RemoveFromRoleAsync(netUser, "User");
                        await _userManager.AddToRoleAsync(netUser, "Admin");
                    }
                    await _userManager.UpdateAsync(netUser);

                    _context.Update(userRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            List<SelectListItem> list = new List<SelectListItem> {
                new SelectListItem() { Text = "User", Value = "User" },
                new SelectListItem() { Text = "Admin", Value = "Admin" }
            };

            ViewData["Roles"] = new SelectList(list, "Value", "Text");
            return View(user);
        }

        // GET: Users/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.Where(u => u.User == id).FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{user.Id}'.");
            }

            await _userManager.SetLockoutEnabledAsync(user, true); //ativa o bloqueio
            await _userManager.SetLockoutEndDateAsync(user, DateTime.Now.AddYears(9)); //bloqueia

            return Redirect("~/");
        }

        private bool UsersExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }

    }
}
