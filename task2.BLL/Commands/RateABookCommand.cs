using MediatR;

namespace task2.BLL.Commands;

public class RateABookCommand:IRequest
{
    public int BookId { get; set; }
    public int Score { get; set; }
}
