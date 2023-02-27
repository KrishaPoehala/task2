using FluentValidation;
using MediatR;
using task2.BLL.Commands;
using task2.Infrastructure.Services.Abstration;

namespace task2.BLL.Handlers;

public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand>
{
    private readonly IValidator<DeleteBookCommand> _validator;
    private readonly IBookService _bookService;

    public DeleteBookCommandHandler(IValidator<DeleteBookCommand> validator, IBookService bookService)
    {
        _validator = validator;
        _bookService = bookService;
    }

    public async Task Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var result = await _validator.ValidateAsync(request, cancellationToken);
        if(result.IsValid == false)
        {
            throw new InvalidDataException(string.Join("\n", result.Errors.Select(x => x.ErrorMessage)));
        }

        await _bookService.Delete(request.Id);
    }
}
