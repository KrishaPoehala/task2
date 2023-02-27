namespace task2.Infrastructure.Dtos;

public record BookWithReviewsDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Cover { get; set; }
    public string Content { get; set; }
    public decimal Rating { get; set; }
    public string Genre { get; set; }
    public ICollection<ReviewDto> Reviews { get; set; }
    public BookWithReviewsDto()
    {
    }
}
