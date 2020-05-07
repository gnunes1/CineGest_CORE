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
    public class Room_MovieController : Controller
    {
        private readonly CineGestDB _context;

        public Room_MovieController(CineGestDB context)
        {
            _context = context;
        }

        // GET: Room_Movie
        public async Task<IActionResult> Index()
        {
            var cineGestDB = _context.Room_Movie.Include(r => r.Movie);
            return View(await cineGestDB.ToListAsync());
        }

        // GET: Room_Movie/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room_Movie = await _context.Room_Movie
                .Include(r => r.Movie)
                .FirstOrDefaultAsync(m => m.MovieFK == id);
            if (room_Movie == null)
            {
                return NotFound();
            }

            return View(room_Movie);
        }

        // GET: Room_Movie/Create
        public IActionResult Create()
        {
            ViewData["MovieFK"] = new SelectList(_context.Movie, "Id", "Name");
            return View();
        }

        // POST: Room_Movie/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovieFK,RoomFK,Room,Schedule")] Room_Movie room_Movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(room_Movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MovieFK"] = new SelectList(_context.Movie, "Id", "Name", room_Movie.MovieFK);
            return View(room_Movie);
        }

        // GET: Room_Movie/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room_Movie = await _context.Room_Movie.FindAsync(id);
            if (room_Movie == null)
            {
                return NotFound();
            }
            ViewData["MovieFK"] = new SelectList(_context.Movie, "Id", "Name", room_Movie.MovieFK);
            return View(room_Movie);
        }

        // POST: Room_Movie/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovieFK,RoomFK,Room,Schedule")] Room_Movie room_Movie)
        {
            if (id != room_Movie.MovieFK)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(room_Movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Room_MovieExists(room_Movie.MovieFK))
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
            ViewData["MovieFK"] = new SelectList(_context.Movie, "Id", "Name", room_Movie.MovieFK);
            return View(room_Movie);
        }

        // GET: Room_Movie/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room_Movie = await _context.Room_Movie
                .Include(r => r.Movie)
                .FirstOrDefaultAsync(m => m.MovieFK == id);
            if (room_Movie == null)
            {
                return NotFound();
            }

            return View(room_Movie);
        }

        // POST: Room_Movie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var room_Movie = await _context.Room_Movie.FindAsync(id);
            _context.Room_Movie.Remove(room_Movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Room_MovieExists(int id)
        {
            return _context.Room_Movie.Any(e => e.MovieFK == id);
        }
    }
}
