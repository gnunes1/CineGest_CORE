using cinegest.Data;
using CineGest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace cinegest.Controllers
{
    public class SessionsController : Controller
    {
        private readonly CinegestDB _context;

        public SessionsController(CinegestDB context)
        {
            _context = context;
        }

        // GET: Sessions
        public async Task<IActionResult> Index()
        {
            var cinegestDB = _context.Sessions.Include(s => s.Cinema).Include(s => s.Movie);
            return View(await cinegestDB.ToListAsync());
        }

        // GET: Sessions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sessions = await _context.Sessions
                .Include(s => s.Cinema)
                .Include(s => s.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sessions == null)
            {
                return NotFound();
            }

            return View(sessions);
        }

        // GET: Sessions/Create
        public IActionResult Create()
        {
            ViewData["CinemaFK"] = new SelectList(_context.Cinemas, "Id", "Id");
            ViewData["MovieFK"] = new SelectList(_context.Movies, "Id", "Id");
            return View();
        }

        // POST: Sessions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CinemaFK,MovieFK,Start,End,Occupated_seats")] Sessions sessions)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sessions);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CinemaFK"] = new SelectList(_context.Cinemas, "Id", "Id", sessions.CinemaFK);
            ViewData["MovieFK"] = new SelectList(_context.Movies, "Id", "Id", sessions.MovieFK);
            return View(sessions);
        }

        // GET: Sessions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sessions = await _context.Sessions
                .Include(s => s.Cinema)
                .Include(s => s.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sessions == null)
            {
                return NotFound();
            }

            return View(sessions);
        }

        // POST: Sessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sessions = await _context.Sessions.FindAsync(id);
            _context.Sessions.Remove(sessions);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SessionsExists(int id)
        {
            return _context.Sessions.Any(e => e.Id == id);
        }
    }
}
