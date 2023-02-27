using MediatR;
using task2.Infrastructure.Dtos;

namespace task2.BLL.Queries;

public class GetDetailsQuery:IRequest<BookWithReviewsDto>
{
    public int Id { get; set; }
}
