using MediatR;
using task2.Infrastructure.Dtos;

namespace task2.BLL.Queries;

public class GetOrderedBooksQuery:IRequest<IEnumerable<BookDto>>
{
    public string OrderPropName { get; set; } = null!;
}
