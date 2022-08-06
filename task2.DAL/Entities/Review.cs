namespace task2.DAL.Entities;

public class Review
{
    public int Id { get; set; }
    public string Message { get; set; }
    public string Reviewer { get; set; }
    public int BookId { get; set; }
    public virtual Book Book { get; set; }
}
