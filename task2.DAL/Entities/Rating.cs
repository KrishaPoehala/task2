using System.ComponentModel.DataAnnotations;

namespace task2.DAL.Entities;

public class Rating
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public virtual Book Book { get; set; }
    [Range(1,6)]
    public int Score { get; set; }
}
