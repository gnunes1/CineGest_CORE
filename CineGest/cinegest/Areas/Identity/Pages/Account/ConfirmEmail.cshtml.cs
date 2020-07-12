using cinegest.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Threading.Tasks;

namespace cinegest.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly CinegestDB _context;

        public ConfirmEmailModel(UserManager<ApplicationUser> userManager, CinegestDB context)
        {
            _userManager = userManager;
            _context = context;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Não foi possivel encontrar o utilizador com o ID '{userId}'.");
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);

            if (result.Succeeded)
            {
                StatusMessage = "Obrigado por confirmar o seu email.";

                await _userManager.SetUserNameAsync(user, await _userManager.GetEmailAsync(user));
                await _userManager.UpdateAsync(user);

                var bdUser = _context.User.Find(user.User);
                bdUser.Email = await _userManager.GetEmailAsync(user);
                await _context.SaveChangesAsync();
            }
            else
            {
                StatusMessage = "Erro ao confirmar o email.";
            }
            return Page();
        }
    }
}
