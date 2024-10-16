// MovieModel.cs
using System.ComponentModel.DataAnnotations;

public class MovieModel
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Title { get; set; }

    [Required]
    public string Genre { get; set; }

    [Required, Range(30, 300)]
    public int Duration { get; set; }
}
