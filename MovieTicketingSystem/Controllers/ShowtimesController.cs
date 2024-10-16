using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using MovieTicketingSystem.Models;

public class ShowtimeController : Controller
{
    // Simulate a data store with a static list for Showtimes
    private static List<ShowtimeModel> showtimes = new List<ShowtimeModel>
    {
        new ShowtimeModel { Id = 1, MovieId = 1, Showtime = DateTime.Now.AddHours(2), AvailableSeats = 50 },
        new ShowtimeModel { Id = 2, MovieId = 2, Showtime = DateTime.Now.AddHours(5), AvailableSeats = 30 }
    };

    // GET: Showtime
    public IActionResult Index()
    {
        return View(showtimes); // Return the list of showtimes (Views/Showtime/Index.cshtml)
    }

    // GET: Showtime/Create
    public IActionResult Create()
    {
        return View(); // Return the create view (Views/Showtime/Create.cshtml)
    }

    // POST: Showtime/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("Id,MovieId,Showtime,AvailableSeats")] ShowtimeModel showtimeModel)
    {
        if (ModelState.IsValid)
        {
            showtimeModel.Id = showtimes.Any() ? showtimes.Max(s => s.Id) + 1 : 1; // Generate new ID
            showtimes.Add(showtimeModel);
            return RedirectToAction(nameof(Index)); // Redirect to index after creation
        }
        return View(showtimeModel); // Return the view if model state is invalid
    }

    // GET: Showtime/Edit/1
    public IActionResult Edit(int id)
    {
        var showtimeModel = showtimes.FirstOrDefault(s => s.Id == id);
        if (showtimeModel == null) return NotFound();
        return View(showtimeModel); // Return the edit view for the specific showtime (Views/Showtime/Edit.cshtml)
    }

    // POST: Showtime/Edit/1
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("Id,MovieId,Showtime,AvailableSeats")] ShowtimeModel showtimeModel)
    {
        if (ModelState.IsValid)
        {
            var existingShowtime = showtimes.FirstOrDefault(s => s.Id == id);
            if (existingShowtime == null) return NotFound();

            // Update the showtime properties
            existingShowtime.MovieId = showtimeModel.MovieId;
            existingShowtime.Showtime = showtimeModel.Showtime;
            existingShowtime.AvailableSeats = showtimeModel.AvailableSeats;

            return RedirectToAction(nameof(Index)); // Redirect to index after update
        }
        return View(showtimeModel); // Return the view if model state is invalid
    }

    // GET: Showtime/Delete/1
    public IActionResult Delete(int id)
    {
        var showtimeModel = showtimes.FirstOrDefault(s => s.Id == id);
        if (showtimeModel == null) return NotFound();
        return View(showtimeModel); // Return the delete confirmation view (Views/Showtime/Delete.cshtml)
    }

    // POST: Showtime/Delete/1
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var showtimeModel = showtimes.FirstOrDefault(s => s.Id == id);
        if (showtimeModel != null) showtimes.Remove(showtimeModel); // Remove the showtime
        return RedirectToAction(nameof(Index)); // Redirect to index after deletion
    }
}
