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
    public class TicketsController : Controller
    {
        private readonly CineGestDB _context;

        public TicketsController(CineGestDB context)
        {
            _context = context;
        }

        // GET: Tickets
        public async Task<IActionResult> Index()
        {
            var cineGestDB = _context.Ticket.Include(t => t.Cinema_Movie).Include(t => t.User);
            return View(await cineGestDB.ToListAsync());
        }

        // GET: Tickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tickets = await _context.Ticket
                .Include(t => t.Cinema_Movie)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tickets == null)
            {
                return NotFound();
            }

            return View(tickets);
        }

        // GET: Tickets/Create
        public IActionResult Create()
        {
            ViewData["Cinema_MovieFK"] = new SelectList(_context.Cinema_Movie, "Id", "Id");
            ViewData["UserFK"] = new SelectList(_context.User, "Id", "Name");
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Cinema_MovieFK,UserFK,Seat")] Tickets tickets)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tickets);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Cinema_MovieFK"] = new SelectList(_context.Cinema_Movie, "Id", "Id", tickets.Cinema_MovieFK);
            ViewData["UserFK"] = new SelectList(_context.User, "Id", "Name", tickets.UserFK);
            return View(tickets);
        }

        // GET: Tickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tickets = await _context.Ticket.FindAsync(id);
            if (tickets == null)
            {
                return NotFound();
            }
            ViewData["Cinema_MovieFK"] = new SelectList(_context.Cinema_Movie, "Id", "Id", tickets.Cinema_MovieFK);
            ViewData["UserFK"] = new SelectList(_context.User, "Id", "Name", tickets.UserFK);
            return View(tickets);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Cinema_MovieFK,UserFK,Seat")] Tickets tickets)
        {
            if (id != tickets.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tickets);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketsExists(tickets.Id))
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
            ViewData["Cinema_MovieFK"] = new SelectList(_context.Cinema_Movie, "Id", "Id", tickets.Cinema_MovieFK);
            ViewData["UserFK"] = new SelectList(_context.User, "Id", "Name", tickets.UserFK);
            return View(tickets);
        }

        // GET: Tickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tickets = await _context.Ticket
                .Include(t => t.Cinema_Movie)
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
            var tickets = await _context.Ticket.FindAsync(id);
            _context.Ticket.Remove(tickets);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketsExists(int id)
        {
            return _context.Ticket.Any(e => e.Id == id);
        }
    }
}
