using cinegest.Data;
using CineGest.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace cinegest.Controllers
{
    public class TicketsController : Controller
    {
        private readonly CinegestDB _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public TicketsController(CinegestDB context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Tickets
        public async Task<IActionResult> Index()
        {
            var cinegestDB = _context.Tickets.Include(t => t.Session).Include(t => t.User);
            return View(await cinegestDB.ToListAsync());
        }

        /// <summary>
        /// GET: MyTickets
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> MyTickets()
        {
            var cinegestDB = _context.Tickets
                .Where(s => s.User.ApplicationUser == _userManager.GetUserId(User))
                .Include(t => t.Session)
                .Include(t => t.Session).ThenInclude(s => s.Movie)
                .Include(t => t.Session).ThenInclude(s => s.Cinema);
            return View(await cinegestDB.ToListAsync());
        }

        // GET: Tickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tickets = await _context.Tickets
                .Include(t => t.Session)
                    .ThenInclude(s => s.Cinema)
                .Include(t => t.Session)
                    .ThenInclude(s => s.Movie)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tickets == null)
            {
                return NotFound();
            }

            return View(tickets);
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int SessionFK)
        {
            var session = _context.Sessions.Find(SessionFK);

            if (session == null)
            {
                return BadRequest();
            }

            ViewData["User"] = _context.User.Where(u => u.ApplicationUser == _userManager.GetUserId(User)).Select(u => u.Id).FirstOrDefault();

            var movie = await _context.Movies
                           .Include(m => m.SessionsList)
                               .ThenInclude(s => s.Cinema)
                           .Where(m => m.Id == session.MovieFK)
                           .FirstOrDefaultAsync();

            //a sessão já começou
            var now = new DateTime().Ticks;
            if (now >= session.Start.Ticks && now <= session.End.Ticks)
            {
                return RedirectToAction("Details", "Movies", movie);
            }

            //o utilizador já tem o bilhete
            if (_context.Tickets.Where(t => t.Session == session && t.User.ApplicationUser == _userManager.GetUserId(User)).Any())
            {
                return RedirectToAction("Details", "Movies", movie);
            }

            //capacidade cheia
            if (session.Occupated_seats >= _context.Cinemas.Find(session.CinemaFK).Capacity)
            {
                return RedirectToAction("Details", "Movies", movie);
            }

            try
            {//compra o bilhete
                Tickets ticket = new Tickets
                {
                    SessionFK = SessionFK,
                    UserFK = _context.User.Where(u => u.ApplicationUser == _userManager.GetUserId(User)).Select(u => u.Id).FirstOrDefault(),
                    Seat = session.Occupated_seats + 1
                };

                session.Occupated_seats += 1;

                _context.Add(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Movies", movie);
            }
            catch (Exception)
            {
                return RedirectToAction("Details", "Movies", movie);
            }
        }

        // GET: Tickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tickets = await _context.Tickets
                .Include(t => t.Session)
                    .ThenInclude(s => s.Cinema)
                .Include(t => t.Session)
                    .ThenInclude(s => s.Movie)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tickets == null)
            {
                return NotFound();
            }

            return View(tickets);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);

            var session = _context.Sessions.Find(ticket.Session);
            if (session == null) return View(nameof(Delete));

            session.Occupated_seats = session.Occupated_seats - 1;

            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketsExists(int id)
        {
            return _context.Tickets.Any(e => e.Id == id);
        }
    }
}
