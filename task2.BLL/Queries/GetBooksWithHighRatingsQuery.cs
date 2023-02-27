using MediatR;
using task2.Infrastructure.Dtos;

namespace task2.BLL.Queries;

public class GetBooksWithHighRatingsQuery:IRequest<IEnumerable<BookDto>>
{
    public string Genre { get; set; }
}
