using cinegest.Data;
using CineGest.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cinegest.Controllers
{
    public class UsersController : Controller
    {
        private readonly CinegestDB _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(CinegestDB context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.User.Where(u => u.ApplicationUser != _userManager.GetUserId(User)).ToListAsync());
        }

        // GET: Users/Details/5
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

        // GET: Users/Create
        public IActionResult Create()
        {
            List<SelectListItem> list = new List<SelectListItem> {
                new SelectListItem() { Text = "User", Value = "User" },
                new SelectListItem() { Text = "Admin", Value = "Admin" }
            };

            ViewData["Roles"] = new SelectList(list, "Value", "Text");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,Name,DoB,Avatar,Role,ApplicationUser")] Users user)
        {
            if (ModelState.IsValid)
            {
                if (_context.User.Where(u => u.Email == user.Email).Any())//verifica se existe algum utilizador com o mesmo email
                {
                    ViewData["message"] = "Já existe um utilizador com o mesmo email.";
                    return View(user);
                }

                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.User.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }

            List<SelectListItem> list = new List<SelectListItem> {
                new SelectListItem() { Text = "User", Value = "User" },
                new SelectListItem() { Text = "Admin", Value = "Admin" }
            };

            ViewData["Roles"] = new SelectList(list, "Value", "Text");
            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,Name,DoB,Avatar,Role,ApplicationUser")] Users user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (_context.User.Where(u => u.Email == user.Email && u.Id != user.Id).Any())//verifica se existe algum utilizador com o mesmo email
                {
                    ViewData["message"] = "Já existe um utilizador com o mesmo email.";
                    return View(user);
                }

                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(user.Id))
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
            return View(user);
        }

        // GET: Users/Delete/5
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
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var users = await _context.User.FindAsync(id);
            _context.User.Remove(users);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsersExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }

    }
}
