using cinegest.Data;
using CineGest.Models;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            //dados dos bilhetes de todos os utilizadores
            var tickets = _context.Tickets.Include(t => t.Session)
                .Include(t => t.Session).ThenInclude(s => s.Movie)
                .Include(t => t.Session).ThenInclude(s => s.Cinema)
                .Include(t => t.User);
            return View(await tickets.ToListAsync());
        }

        /// <summary>
        /// GET: MyTickets
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> MyTickets()
        {
            var user = await _userManager.GetUserAsync(User);

            //dados dos bilhetes do proprio utilizador
            var cinegestDB = _context.Tickets
                .Where(s => s.User.Id == user.User)
                .Include(t => t.Session)
                .Include(t => t.Session).ThenInclude(s => s.Movie)
                .Include(t => t.Session).ThenInclude(s => s.Cinema);
            return View(await cinegestDB.ToListAsync());
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Create(int SessionFK)
        {
            var session = _context.Sessions.Find(SessionFK);

            if (session == null)
            {
                return RedirectToAction("Index", "Home");
            }
            //obtem o application user atual
            var applicationUser = await _userManager.GetUserAsync(User);

            //obtem o utilizador associado ao application user atual
            var user = _context.User.Where(u => u.Id == applicationUser.User).FirstOrDefault();

            ViewData["User"] = user.Id;//envia para a view o id do user

            //filme da sessao escolhida pelo utilizador
            var movie = await _context.Movies.Where(m => m.Id == session.MovieFK).FirstOrDefaultAsync();

            //a sessão já começou
            var now = new DateTime().Ticks;
            if (now >= session.Start.Ticks && now <= session.End.Ticks)
            {
                TempData["Message"] = "Esta sessão já está a decorrer.";
                return Redirect("~/Movies/Details/" + movie.Id);
            }

            //o utilizador já tem o bilhete
            if (_context.Tickets.Where(t => t.Session == session && t.Id == applicationUser.User).Any())
            {
                TempData["Message"] = "Já possui o bilhete para esta sessão.";
                return Redirect("~/Movies/Details/" + movie.Id);
            }

            //capacidade cheia
            if (session.Occupated_seats >= _context.Cinemas.Find(session.CinemaFK).Capacity)
            {
                TempData["Message"] = "Filme com capacidade esgotada. Atualize a página.";
                return Redirect("~/Movies/Details/" + movie.Id);
            }

            //sem idade para comprar o bilhete
            if (DateTime.Now.Ticks < user.DoB.AddYears(session.Movie.Min_age).Ticks)
            {
                TempData["Message"] = "Não tem idade para comprar o bilhete para este filme.";
                return Redirect("~/Movies/Details/" + movie.Id);
            }

            try
            {//compra o bilhete
                Tickets ticket = new Tickets
                {

                    SessionFK = SessionFK,
                    UserFK = _context.User.Where(u => u.Id == applicationUser.User).Select(u => u.Id).FirstOrDefault(),
                    Seat = session.Occupated_seats + 1
                };

                session.Occupated_seats += 1;//incrementa o numero de lugares ocupados na sessao

                _context.Add(ticket);
                await _context.SaveChangesAsync();
                return Redirect("~/Movies/Details/" + movie.Id);
            }
            catch (Exception)
            {
                TempData["Message"] = "Erro inesperado.";
                return Redirect("~/Movies/Details/" + movie.Id);
            }
        }

        // GET: Tickets/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return View(nameof(Index));
            }

            var ticket = await _context.Tickets
                .Include(t => t.Session)
                    .ThenInclude(s => s.Cinema)
                .Include(t => t.Session)
                    .ThenInclude(s => s.Movie)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return View(nameof(Index));
            }

            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return View(nameof(Index));
            }

            var session = await _context.Sessions.FindAsync(ticket.SessionFK);
            if (session == null)
            {
                ViewData["message"] = "Não foi encontrada nenhuma sessão com este Id";
                return View(nameof(Delete));
            }

            session.Occupated_seats = session.Occupated_seats - 1;//decrementa o numeros e lugares ocupados

            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();

            return Redirect("~/Tickets");
        }

        private bool TicketsExists(int id)
        {
            return _context.Tickets.Any(e => e.Id == id);
        }
    }
}
