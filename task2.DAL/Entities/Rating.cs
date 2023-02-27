using System.ComponentModel.DataAnnotations;

namespace task2.DAL.Entities;

public class Rating
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public  Book Book { get; set; }
    public int Score { get; set; }
}
