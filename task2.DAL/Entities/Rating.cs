namespace task2.DAL.Entities;

public class Rating
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public virtual Book Book { get; set; }
    public int Score { get; set; }
}
