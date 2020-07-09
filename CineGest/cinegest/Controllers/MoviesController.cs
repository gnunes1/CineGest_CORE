using cinegest.Data;
using CineGest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace cinegest.Controllers
{
    public class MoviesController : Controller
    {
        /// <summary>
        /// Defines the _environment.
        /// </summary>
        public static IWebHostEnvironment _environment;

        private readonly CinegestDB _context;

        public MoviesController(CinegestDB context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Movies
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Movies.ToListAsync());
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                RedirectToAction(nameof(Index));
            }

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                RedirectToAction(nameof(Index));
            }

            return View(movie);
        }

        // GET: Movies/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Genres,Duration,Min_age,Highlighted")] Movies movie, IFormFile Poster)
        {
            if (ModelState.IsValid)
            {
                if (_context.Movies.Where(m => m.Name == movie.Name).Any())//verifica se existe algum filme com o mesmo nome
                {
                    ViewData["message"] = "Já existe um filme com o mesmo nome.";
                    return View(movie);
                }
                //se o poster não fôr imagem usa-se a imagem default
                if (Poster == null || !Poster.ContentType.Contains("image"))
                {
                    movie.Poster = "default.png";

                }
                else
                { //usa a imagem recebida
                    Guid g;
                    g = Guid.NewGuid();

                    string extensao = Path.GetExtension(Poster.FileName).ToLower();

                    // caminho do ficheiro 
                    movie.Poster = g.ToString() + extensao;

                    //guarda a imagem
                    using var fileStream = new FileStream(_environment.WebRootPath + "/images/movies/" + g.ToString() + extensao, FileMode.Create);
                    await Poster.CopyToAsync(fileStream);
                }
                //adiciona o filme ----------------------
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                RedirectToAction(nameof(Index));
            }

            var movies = await _context.Movies.FindAsync(id);
            if (movies == null)
            {
                RedirectToAction(nameof(Index));
            }
            return View(movies);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, int Id, string Name, string Description, string Genres, int Duration, int Min_age, bool Highlighted, IFormFile Poster)
        {
            Movies movie = _context.Movies.Find(Id);

            if (id != movie.Id)
            {
                RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                if (_context.Movies.Where(m => m.Name == movie.Name && m.Id != movie.Id).Any())//verifica se existe algum filme com o mesmo nome
                {
                    ViewData["message"] = "Já existe um filme com o mesmo nome.";
                    return View(movie);
                }

                try
                {
                    //verifica se há sessões ativas para o filme
                    if (await _context.Sessions.Where(s => s.Movie.Id == id).AnyAsync())
                    {
                        ViewData["message"] = "Não é possivel alterar o filme, tendo sessões agendadas para o mesmo.";
                        return View(movie);
                    }

                    //verifica se o cartaz mudou
                    if (Poster == null)
                    { } //não muda a imagem
                    else if (!Poster.ContentType.Contains("image")) //se o poster não fôr imagem usa-se a imagem default
                    {
                        //apaga a antiga imagem
                        if (movie.Poster != "default.png") System.IO.File.Delete(_environment.WebRootPath + "/images/movies/" + movie.Poster);

                        //guarda a nova imagem
                        movie.Poster = "default.png";
                        await _context.SaveChangesAsync();

                    }
                    else
                    { //update atualiza a imagem

                        //apaga o antigo poster excepto se for o poster default
                        if (movie.Poster != "default.png") System.IO.File.Delete(_environment.WebRootPath + "/images/movies/" + movie.Poster);

                        Guid g;
                        g = Guid.NewGuid();

                        string extensao = Path.GetExtension(Poster.FileName).ToLower();

                        // caminho do ficheiro 
                        movie.Poster = g.ToString() + extensao;

                        //guarda a imagem
                        using var fileStream = new FileStream(_environment.WebRootPath + "/images/movies/" + g.ToString() + extensao, FileMode.Create);
                        await Poster.CopyToAsync(fileStream);
                    }

                    movie.Name = Name;
                    movie.Description = Description;
                    movie.Genres = Genres;
                    movie.Duration = Duration;
                    movie.Highlighted = Highlighted;
                    movie.Min_age = Min_age;

                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MoviesExists(movie.Id))
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
            return View(movie);
        }

        // GET: Movies/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                RedirectToAction(nameof(Index));
            }

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                RedirectToAction(nameof(Index));
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            //verifica se há sessão para o filme
            if (await _context.Sessions.Where(s => s.Cinema.Id == id).AnyAsync())
            {
                ViewData["message"] = "Não é possivel apagar o filme, tendo sessões agendadas para o mesmo.";
                return View(movie);
            }

            _context.Movies.Remove(movie);

            var sessions = await _context.Sessions.Where(s => s.Movie == movie).ToListAsync();
            foreach (var item in sessions) //apaga todas as sessions
            {
                _context.Sessions.Remove(item);
            }

            //apaga o cartaz associado ao filme
            if (movie.Poster.ToString() != "default.png")
                System.IO.File.Delete(_environment.WebRootPath + "/images/movies/" + movie.Poster.ToString());

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MoviesExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}
