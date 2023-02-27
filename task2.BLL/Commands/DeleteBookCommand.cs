using MediatR;

namespace task2.BLL.Commands;

public class DeleteBookCommand: IRequest
{
    public int Id { get; set; }
}
