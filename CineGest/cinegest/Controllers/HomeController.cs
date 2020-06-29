using cinegest.Data;
using cinegest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace cinegest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CinegestDB _context;

        public HomeController(ILogger<HomeController> logger, CinegestDB context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var highlighted = await _context.Movies.Where(m => m.Highlighted == true)
                .Select(m => new ArrayList
                {
                        m.Id,
                        m.Poster,
                        m.Name,
                        _context.Sessions.Where(s => s.Movie.Id == m.Id).Min(s => s.Start).ToString("dd-MM-yyyy"),
                        _context.Sessions.Where(s => s.Movie.Id == m.Id).Max(s => s.End).ToString("dd-MM-yyyy")
                })
                .Distinct().ToListAsync();

            ViewBag.Highlighted = highlighted;
            ViewBag.Movies = await _context.Movies.Select(m => new ArrayList { m.Id, m.Name, m.Poster }).ToListAsync();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
