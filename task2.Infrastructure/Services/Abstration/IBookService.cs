using task2.Infrastructure.Dtos;

namespace task2.Infrastructure.Services.Abstration;

public interface IBookService
{
    Task<IEnumerable<BookDto>> Get(string orderPropName);
    Task<IEnumerable<BookDto>> GetBooksWithHighRatings(string? genre);
    Task<BookWithReviewsDto> GetDetails(int id);
    Task Delete(int id);
    Task<int> Save(NewBookDto dto);
    Task<int> SaveReview(NewReviewDto dto);
    Task RateABook(NewScoreDto dto);
}
