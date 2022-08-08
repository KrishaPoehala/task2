using System.ComponentModel.DataAnnotations;

namespace task2.DAL.Entities;

public class Book
{
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    [MaxLength(100)]
    public string Cover { get; set; }
    [Required]

    public string Content { get; set; }
    [MaxLength(100)]
    public string Author { get; set; }
    [MaxLength(100)]
    public string Genre { get; set; }
    public virtual ICollection<Rating> Ratings { get; set; } = new LinkedList<Rating>();
    public virtual ICollection<Review> Reviews { get; set; } = new LinkedList<Review>();
}
