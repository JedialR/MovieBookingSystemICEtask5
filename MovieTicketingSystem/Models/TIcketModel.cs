// TicketModel.cs
using System.ComponentModel.DataAnnotations;

public class TicketModel
{
    public int Id { get; set; }

    [Required]
    public int ShowtimeId { get; set; }

    [Required, MaxLength(100)]
    public string CustomerName { get; set; }

    [Required, Range(1, 10)]
    public int NumberOfTickets { get; set; }
}
