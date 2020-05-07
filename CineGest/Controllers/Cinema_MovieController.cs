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
    public class Cinema_MovieController : Controller
    {
        private readonly CineGestDB _context;

        public Cinema_MovieController(CineGestDB context)
        {
            _context = context;
        }

        // GET: Cinema_Movie
        public async Task<IActionResult> Index()
        {
            var cineGestDB = _context.Cinema_Movie.Include(c => c.Cinema).Include(c => c.Movie);
            return View(await cineGestDB.ToListAsync());
        }

        // GET: Cinema_Movie/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cinema_Movie = await _context.Cinema_Movie
                .Include(c => c.Cinema)
                .Include(c => c.Movie)
                .FirstOrDefaultAsync(m => m.MovieFK == id);
            if (cinema_Movie == null)
            {
                return NotFound();
            }

            return View(cinema_Movie);
        }

        // GET: Cinema_Movie/Create
        public IActionResult Create()
        {
            ViewData["CinemaFK"] = new SelectList(_context.Cinema, "Id", "Name");
            ViewData["MovieFK"] = new SelectList(_context.Movie, "Id", "Name");
            return View();
        }

        // POST: Cinema_Movie/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovieFK,CinemaFK")] Cinema_Movie cinema_Movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cinema_Movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CinemaFK"] = new SelectList(_context.Cinema, "Id", "Name", cinema_Movie.CinemaFK);
            ViewData["MovieFK"] = new SelectList(_context.Movie, "Id", "Name", cinema_Movie.MovieFK);
            return View(cinema_Movie);
        }

        // GET: Cinema_Movie/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cinema_Movie = await _context.Cinema_Movie.FindAsync(id);
            if (cinema_Movie == null)
            {
                return NotFound();
            }
            ViewData["CinemaFK"] = new SelectList(_context.Cinema, "Id", "Name", cinema_Movie.CinemaFK);
            ViewData["MovieFK"] = new SelectList(_context.Movie, "Id", "Name", cinema_Movie.MovieFK);
            return View(cinema_Movie);
        }

        // POST: Cinema_Movie/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovieFK,CinemaFK")] Cinema_Movie cinema_Movie)
        {
            if (id != cinema_Movie.MovieFK)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cinema_Movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Cinema_MovieExists(cinema_Movie.MovieFK))
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
            ViewData["CinemaFK"] = new SelectList(_context.Cinema, "Id", "Name", cinema_Movie.CinemaFK);
            ViewData["MovieFK"] = new SelectList(_context.Movie, "Id", "Name", cinema_Movie.MovieFK);
            return View(cinema_Movie);
        }

        // GET: Cinema_Movie/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cinema_Movie = await _context.Cinema_Movie
                .Include(c => c.Cinema)
                .Include(c => c.Movie)
                .FirstOrDefaultAsync(m => m.MovieFK == id);
            if (cinema_Movie == null)
            {
                return NotFound();
            }

            return View(cinema_Movie);
        }

        // POST: Cinema_Movie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cinema_Movie = await _context.Cinema_Movie.FindAsync(id);
            _context.Cinema_Movie.Remove(cinema_Movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Cinema_MovieExists(int id)
        {
            return _context.Cinema_Movie.Any(e => e.MovieFK == id);
        }
    }
}
