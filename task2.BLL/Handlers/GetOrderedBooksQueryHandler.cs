using FluentValidation;
using MediatR;
using task2.BLL.Queries;
using task2.Infrastructure.Dtos;
using task2.Infrastructure.Services.Abstration;

namespace task2.BLL.Handlers;

public class GetOrderedBooksQueryHandler : IRequestHandler<GetOrderedBooksQuery, IEnumerable<BookDto>>
{
    private readonly IValidator<GetOrderedBooksQuery> _validator;
    private readonly IBookService _bookService;
    public GetOrderedBooksQueryHandler(IValidator<GetOrderedBooksQuery> validator, IBookService bookService)
    {
        _validator = validator;
        _bookService = bookService;
    }
    public async Task<IEnumerable<BookDto>> Handle(GetOrderedBooksQuery request, CancellationToken cancellationToken)
    {
        var result = await _validator.ValidateAsync(request, cancellationToken);
        if (result.IsValid)
        {
            return await _bookService.Get(request.OrderPropName);
        }

        throw new InvalidDataException(string.Join("\n", result.Errors.Select(x => x.ErrorMessage)));
    }
}
