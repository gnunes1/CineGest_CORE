using cinegest.Data;
using CineGest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
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
                RedirectToAction(nameof(Index));
            }

            var session = await _context.Sessions
                .Include(s => s.Cinema)
                .Include(s => s.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (session == null)
            {
                RedirectToAction(nameof(Index));
            }

            return View(session);
        }

        // GET: Sessions/Create
        public IActionResult Create()
        {
            ViewData["CinemaFK"] = new SelectList(_context.Cinemas, "Id", "Name");
            ViewData["MovieFK"] = new SelectList(_context.Movies, "Id", "Name");
            return View();
        }

        // POST: Sessions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CinemaFK,MovieFK,Start,End,Occupated_seats")] Sessions session)
        {
            if (ModelState.IsValid)
            {
                if (await _context.Sessions.Where(s => session.Start >= s.Start && session.Start <= s.End && s.Cinema.Id == session.CinemaFK).AnyAsync())
                {
                    ViewData["message"] = "Já existe uma sessão neste cinema entre esta data.";
                    return View();
                }

                session.End = session.Start; //fim=inicio
                session.End = session.End.AddMinutes(_context.Movies.Find(session.MovieFK).Duration);//+duração do filme

                _context.Add(session);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CinemaFK"] = new SelectList(_context.Cinemas, "Id", "Name", session.CinemaFK);
            ViewData["MovieFK"] = new SelectList(_context.Movies, "Id", "Name", session.MovieFK);
            return View(session);
        }

        // GET: Sessions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                RedirectToAction(nameof(Index));
            }

            var session = await _context.Sessions
                .Include(s => s.Cinema)
                .Include(s => s.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (session == null)
            {
                RedirectToAction(nameof(Index));
            }

            return View(session);
        }

        // POST: Sessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var session = await _context.Sessions.FindAsync(id);

            if (DateTime.UtcNow.Ticks >= session.Start.Ticks && DateTime.UtcNow.Ticks <= session.End.Ticks)
            {
                ViewData["message"] = "Esta sessão está decorrer, não é possivel elimina-la";
                return View(session);
            }

            foreach (var item in session.TicketsList)
            {
                _context.Tickets.Remove(item);
            }

            _context.Sessions.Remove(session);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SessionsExists(int id)
        {
            return _context.Sessions.Any(e => e.Id == id);
        }
    }
}
