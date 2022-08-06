
using task2.Common.Dtos;
namespace task2.BLL.Services.Abstration;

public interface IBookService
{
    IEnumerable<BookDto> Get(string orderPropName);
    IEnumerable<BookDto> GetBooksWithHighRatings();
    Task<BookWithReviewsDto> GetDetails(int id);
    Task Delete(int id);
    Task<int> Save(NewBookDto dto);
}
