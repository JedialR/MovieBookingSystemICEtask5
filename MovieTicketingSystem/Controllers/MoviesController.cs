using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using MovieTicketingSystem.Models;

public class MoviesController : Controller
{
    // Simulated data store using a static list
    private static List<MovieModel> movies = new List<MovieModel>
    {
        new MovieModel { Id = 1, Title = "Inception", Genre = "Sci-Fi", Duration = 148 },
        new MovieModel { Id = 2, Title = "The Matrix", Genre = "Action", Duration = 136 }
    };

    // GET: Movies
    public IActionResult Index()
    {
        return View(movies); // Looks for Views/Movies/Index.cshtml
    }

    // GET: Movies/Create
    public IActionResult Create()
    {
        return View(); // Looks for Views/Movies/Create.cshtml
    }

    // POST: Movies/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("Id,Title,Genre,Duration")] MovieModel movie)
    {
        if (ModelState.IsValid)
        {
            movie.Id = movies.Any() ? movies.Max(m => m.Id) + 1 : 1; // Generate new ID
            movies.Add(movie);
            return RedirectToAction(nameof(Index));
        }
        return View(movie);
    }

    // GET: Movies/Edit/1
    public IActionResult Edit(int id)
    {
        var movie = movies.FirstOrDefault(m => m.Id == id);
        if (movie == null) return NotFound();
        return View(movie); // Looks for Views/Movies/Edit.cshtml
    }

    // POST: Movies/Edit/1
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("Id,Title,Genre,Duration")] MovieModel movie)
    {
        if (ModelState.IsValid)
        {
            var existingMovie = movies.FirstOrDefault(m => m.Id == id);
            if (existingMovie == null) return NotFound();

            existingMovie.Title = movie.Title;
            existingMovie.Genre = movie.Genre;
            existingMovie.Duration = movie.Duration;

            return RedirectToAction(nameof(Index));
        }
        return View(movie);
    }

    // GET: Movies/Delete/1
    public IActionResult Delete(int id)
    {
        var movie = movies.FirstOrDefault(m => m.Id == id);
        if (movie == null) return NotFound();
        return View(movie); // Looks for Views/Movies/Delete.cshtml
    }

    // POST: Movies/Delete/1
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var movie = movies.FirstOrDefault(m => m.Id == id);
        if (movie != null) movies.Remove(movie);
        return RedirectToAction(nameof(Index));
    }
}
