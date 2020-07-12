using cinegest.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cinegest.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly CinegestDB _context;
        private readonly ILogger<LoginModel> _logger;
        public static IWebHostEnvironment _environment;


        public LoginModel(CinegestDB context,
            SignInManager<ApplicationUser> signInManager,
            ILogger<LoginModel> logger,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment environment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
            _environment = environment;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Lembrar os dados de acesso?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);

                Console.WriteLine(result);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");

                    //apaga o utilizador ----------------------------
                    var user = _context.Users.Where(u => u.UserName == Input.Email).FirstOrDefault();

                    var result2 = await _userManager.DeleteAsync(user);
                    var userId = await _userManager.GetUserIdAsync(user);
                    if (!result2.Succeeded)
                    {
                        throw new InvalidOperationException($"Unexpected error occurred deleting user with ID '{userId}'.");
                    }

                    var dbUser = _context.User.Find(user.User);

                    foreach (var item in dbUser.TicketsList) //apaga os bilhetes
                    {
                        dbUser.TicketsList.Remove(item);
                        if (item.Session.Start.Ticks > DateTime.Now.Ticks) item.Session.Occupated_seats -= 1;
                    }

                    _context.User.Remove(dbUser); //apaga o utilizador
                    if (dbUser.Avatar != "default.png") System.IO.File.Delete(_environment.WebRootPath + "/images/users/" + dbUser.Avatar); // apaga a fotografia

                    await _context.SaveChangesAsync();

                    _logger.LogInformation("User with ID '{UserId}' was deleted.", userId);
                    //----------------------------------------

                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Tentativa inválida.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
