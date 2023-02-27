using FluentValidation;
using MediatR;
using task2.BLL.Queries;
using task2.Infrastructure.Dtos;
using task2.Infrastructure.Services.Abstration;

namespace task2.BLL.Handlers;

public class GetDetailsQueryHandler : IRequestHandler<GetDetailsQuery, BookWithReviewsDto>
{
    private readonly IValidator<GetDetailsQuery> _validator;
    private readonly IBookService _bookService;

    public GetDetailsQueryHandler(IValidator<GetDetailsQuery> validator, IBookService bookService)
    {
        _validator = validator;
        _bookService = bookService;
    }

    public async Task<BookWithReviewsDto> Handle(GetDetailsQuery request, CancellationToken cancellationToken)
    {
        var result = await _validator.ValidateAsync(request, cancellationToken);
        if (result.IsValid)
        {
            return await _bookService.GetDetails(request.Id);
        }

        throw new InvalidDataException(string.Join("\n", result.Errors.Select(x => x.ErrorMessage)));
    }
}
