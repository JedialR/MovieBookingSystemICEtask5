using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using MovieTicketingSystem.Models;

public class TicketsController : Controller
{
    // Simulate a data store for Showtimes and Tickets
    private static List<ShowtimeModel> showtimes = new List<ShowtimeModel>
    {
        new ShowtimeModel { Id = 1, MovieId = 1, Showtime = DateTime.Now.AddHours(2), AvailableSeats = 50 },
        new ShowtimeModel { Id = 2, MovieId = 2, Showtime = DateTime.Now.AddHours(5), AvailableSeats = 30 }
    };

    private static List<TicketModel> tickets = new List<TicketModel>();

    // GET: Tickets/Book
    public IActionResult Book(int showtimeId = 1) // Default showtimeId for testing
    {
        var showtime = showtimes.FirstOrDefault(s => s.Id == showtimeId);
        if (showtime == null) return NotFound();

        var ticket = new TicketModel { ShowtimeId = showtimeId };
        return View(ticket); // Return the Book view for ticket booking
    }

    // POST: Tickets/Book
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Book([Bind("Id,ShowtimeId,CustomerName,NumberOfTickets")] TicketModel ticketModel)
    {
        if (ModelState.IsValid)
        {
            var showtime = showtimes.FirstOrDefault(s => s.Id == ticketModel.ShowtimeId);
            if (showtime == null) return NotFound();

            // Check if there are enough available seats
            if (showtime.AvailableSeats < ticketModel.NumberOfTickets)
            {
                ModelState.AddModelError("", "Not enough available seats.");
                return View(ticketModel);
            }

            // Book the tickets and reduce available seats
            showtime.AvailableSeats -= ticketModel.NumberOfTickets;
            ticketModel.Id = tickets.Any() ? tickets.Max(t => t.Id) + 1 : 1; // Generate new ticket ID
            tickets.Add(ticketModel);

            return RedirectToAction(nameof(Confirmation)); // Redirect to confirmation page
        }
        return View(ticketModel); // Return the view if model state is invalid
    }

    // GET: Tickets/Confirmation
    public IActionResult Confirmation()
    {
        return View(); // Return the Confirmation view after successful booking
    }
}
