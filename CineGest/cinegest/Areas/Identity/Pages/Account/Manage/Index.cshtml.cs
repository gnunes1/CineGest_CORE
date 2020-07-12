using cinegest.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;

namespace cinegest.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly CinegestDB _context;
        public static IWebHostEnvironment _environment;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            CinegestDB context,
            IWebHostEnvironment environment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _environment = environment;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [StringLength(40, ErrorMessage = "O {0} não pode ter mais de {1} carateres.")]
            [RegularExpression("[A-ZÓÂÍ][a-zçáéíóúàèìòùãõäëïöüâêîôûñ]+(( | d[ao](s)? | e |-|'| d')[A-ZÓÂÍ][a-zçáéíóúàèìòùãõäëïöüâêîôûñ]+){1,3}",
             ErrorMessage = "Deve escrever entre 2 e 4 nomes, começados por uma maiúscula, seguidos de minúsculas.")]
            public string Nome { get; set; }

            [Required(ErrorMessage = "A data de nascimento é de preenchimento obrigatório.")]
            [Display(Name = "Data de nascimento")]
            [DataType(DataType.Date)]
            public DateTime DoB { get; set; }
        }

        private void LoadAsync(ApplicationUser user)
        {
            Input = new InputModel
            {
                Nome = user.Nome,
                DoB = _context.User.Find(user.User).DoB.Date
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var netUser = await _userManager.GetUserAsync(User);
            if (netUser == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                LoadAsync(netUser);
                return Page();
            }
            var bdUser = await _context.User.FindAsync(netUser.User);

            var file = HttpContext.Request.Form.Files.GetFile("Avatar");

            //verifica se o avatar mudou
            if (file == null)
            { } //não muda a imagem
            else if (!file.ContentType.Contains("image")) //se o poster não fôr imagem usa-se a imagem default
            {
                //apaga a antiga imagem
                if (bdUser.Avatar != "default.png") System.IO.File.Delete(_environment.WebRootPath + "/images/users/" + bdUser.Avatar);

                //guarda a nova imagem
                bdUser.Avatar = "default.png";
            }
            else
            { //update atualiza a imagem

                //apaga o antigo poster excepto se for o poster default
                if (bdUser.Avatar != "default.png") System.IO.File.Delete(_environment.WebRootPath + "/images/users/" + bdUser.Avatar);

                Guid g;
                g = Guid.NewGuid();

                string extensao = Path.GetExtension(file.FileName).ToLower();

                // caminho do ficheiro 
                bdUser.Avatar = g.ToString() + extensao;

                //guarda a imagem
                using var fileStream = new FileStream(_environment.WebRootPath + "/images/users/" + g.ToString() + extensao, FileMode.Create);
                await file.CopyToAsync(fileStream);
            }

            if (Input.Nome != netUser.Nome) //atualiza o nome
            {
                netUser.Nome = Input.Nome;
            }

            if (Input.DoB != bdUser.DoB) //atualiza a data de nascimento
            {
                bdUser.DoB = Input.DoB;
            }

            _context.Update(bdUser); //atualiza a imagem
            await _userManager.UpdateAsync(netUser);
            await _context.SaveChangesAsync();

            await _signInManager.RefreshSignInAsync(netUser);
            StatusMessage = "O seu perfil foi atualizado.";
            return RedirectToPage();
        }
    }
}
