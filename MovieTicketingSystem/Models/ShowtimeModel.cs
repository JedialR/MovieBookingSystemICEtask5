// ShowtimeModel.cs
using System.ComponentModel.DataAnnotations;

public class ShowtimeModel
{
    public int Id { get; set; }

    [Required]
    public int MovieId { get; set; }

    [Required]
    public DateTime Showtime { get; set; }

    [Required, Range(1, 100)]
    public int AvailableSeats { get; set; }
}
