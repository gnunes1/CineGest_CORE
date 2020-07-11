using cinegest.Data;
using CineGest.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace cinegest.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly CinegestDB _context;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            CinegestDB context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        /// classe que identifica os dados a recolher na interface de 'Registo'
        /// </summary>
        public class InputModel
        {
            [Required(ErrorMessage = "O {0} é de preeenchimento obrigatório")]
            [EmailAddress(ErrorMessage = "O {0} está mal escrito...")]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "A {0} deve ter entre {2} e {1} carateres de tamanho.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirmar password")]
            [Compare("Password", ErrorMessage = "A password e a sua confirmação não correspondem.")]
            public string ConfirmPassword { get; set; }

            [Required(ErrorMessage = "O Nome é de preenchimento obrigatório")]
            [StringLength(40, ErrorMessage = "O {0} não pode ter mais de {1} carateres.")]
            [RegularExpression("[A-ZÓÂÍ][a-zçáéíóúàèìòùãõäëïöüâêîôûñ]+(( | d[ao](s)? | e |-|'| d')[A-ZÓÂÍ][a-zçáéíóúàèìòùãõäëïöüâêîôûñ]+){1,3}",
                            ErrorMessage = "Deve escrever entre 2 e 4 nomes, começados por uma maiúscula, seguidos de minúsculas.")]
            public string Nome { get; set; }

            [Required(ErrorMessage = "A data de nascimento é de preenchimento obrigatório.")]
            [Display(Name = "Data de nascimento")]
            public DateTime DoB { get; set; }
        }

        public async Task OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var applicationUser = new ApplicationUser { UserName = Input.Email, Email = Input.Email, Nome = Input.Nome, Timestamp = DateTime.Now };
                var result = await _userManager.CreateAsync(applicationUser, Input.Password);
                if (result.Succeeded)
                {
                    //dar permissão ao Application user
                    await _userManager.AddToRoleAsync(applicationUser, "User");

                    Users user = new Users //cria o utilizador usado nas relações
                    {
                        Name = applicationUser.Nome,
                        Email = applicationUser.Email,
                        DoB = Input.DoB,
                        Role = "User",
                        Avatar = "default.png",
                        ApplicationUser = applicationUser.Id
                    };
                    _context.Add(user);
                    await _context.SaveChangesAsync();

                    _logger.LogInformation("Utilizador criado.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(applicationUser);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = applicationUser.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirme o seu email",
                        $"Por favor confirme o seu email ao <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicar aqui</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(applicationUser, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
