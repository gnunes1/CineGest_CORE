using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CineGest.Data;
using CineGest.Models;

namespace CineGest.Controllers
{
    public class SessionsController : Controller
    {
        private readonly CineGestDB _context;

        public SessionsController(CineGestDB context)
        {
            _context = context;
        }

        // GET: Sessions
        public async Task<IActionResult> Index()
        {
            var cineGestDB = _context.Cinema_Movie.Include(s => s.Cinema).Include(s => s.Movie);
            return View(await cineGestDB.ToListAsync());
        }

        // GET: Sessions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sessions = await _context.Cinema_Movie
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
            ViewData["CinemaFK"] = new SelectList(_context.Cinema, "Id", "City");
            ViewData["MovieFK"] = new SelectList(_context.Movie, "Id", "Name");
            return View();
        }

        // POST: Sessions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CinemaFK,MovieFK,Start_Time,Start,End,Occupated_seats")] Sessions sessions)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sessions);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CinemaFK"] = new SelectList(_context.Cinema, "Id", "City", sessions.CinemaFK);
            ViewData["MovieFK"] = new SelectList(_context.Movie, "Id", "Name", sessions.MovieFK);
            return View(sessions);
        }

        // GET: Sessions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sessions = await _context.Cinema_Movie.FindAsync(id);
            if (sessions == null)
            {
                return NotFound();
            }
            ViewData["CinemaFK"] = new SelectList(_context.Cinema, "Id", "City", sessions.CinemaFK);
            ViewData["MovieFK"] = new SelectList(_context.Movie, "Id", "Name", sessions.MovieFK);
            return View(sessions);
        }

        // POST: Sessions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CinemaFK,MovieFK,Start_Time,Start,End,Occupated_seats")] Sessions sessions)
        {
            if (id != sessions.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sessions);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SessionsExists(sessions.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CinemaFK"] = new SelectList(_context.Cinema, "Id", "City", sessions.CinemaFK);
            ViewData["MovieFK"] = new SelectList(_context.Movie, "Id", "Name", sessions.MovieFK);
            return View(sessions);
        }

        // GET: Sessions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sessions = await _context.Cinema_Movie
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
            var sessions = await _context.Cinema_Movie.FindAsync(id);
            _context.Cinema_Movie.Remove(sessions);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SessionsExists(int id)
        {
            return _context.Cinema_Movie.Any(e => e.Id == id);
        }
    }
}
