using cinegest.Data;
using CineGest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace cinegest.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CinemasController : Controller
    {
        private readonly CinegestDB _context;

        public CinemasController(CinegestDB context)
        {
            _context = context;
        }

        /// <summary>
        /// GET: Cinemas
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cinemas.ToListAsync());
        }

        /// <summary>
        /// GET: Cinemas/Details/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                RedirectToAction(nameof(Index));
            }

            var cinema = await _context.Cinemas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cinema == null)
            {
                RedirectToAction(nameof(Index));
            }

            return View(cinema);
        }

        /// <summary>
        /// GET: Cinemas/Create
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// POST: Cinemas/Create
        /// </summary>
        /// <param name="cinema"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Capacity,City,Location")] Cinemas cinema)
        {

            if (ModelState.IsValid)
            {
                _context.Add(cinema);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cinema);
        }

        // GET: Cinemas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var cinemas = await _context.Cinemas.FindAsync(id);
            if (cinemas == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(cinemas);
        }

        // POST: Cinemas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Capacity,City,Location")] Cinemas cinema)
        {
            if (id != cinema.Id)
            {
                RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cinema);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CinemasExists(cinema.Id))
                    {
                        RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        RedirectToAction(nameof(Index));
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cinema);
        }

        // GET: Cinemas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                RedirectToAction(nameof(Index));
            }

            var cinema = await _context.Cinemas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cinema == null)
            {
                RedirectToAction(nameof(Index));
            }

            return View(cinema);
        }

        // POST: Cinemas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var cinema = await _context.Cinemas.FindAsync(id);

            if (await _context.Sessions.Where(s => s.Cinema.Id == id).AnyAsync())
            {
                ViewData["message"] = "Não é possivel apagar o cinema com sessões agendadas.";
                return View(cinema);
            }

            _context.Cinemas.Remove(cinema);

            var sessions = await _context.Sessions.Where(s => s.Cinema == cinema).ToListAsync();
            foreach (var item in sessions) //apaga todas as sessions
            {
                _context.Sessions.Remove(item);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CinemasExists(int id)
        {
            return _context.Cinemas.Any(e => e.Id == id);
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult VerifyName(string name)
        {
            if (_context.Cinemas.Where(c => c.Name == name).Any())
            {
                return Json($"Já existe um cinema com o mesmo nome.");
            }

            return Json(true);
        }
    }
}
