using AutoMapper;
using FluentValidation;
using MediatR;
using task2.BLL.Commands;
using task2.Infrastructure.Dtos;
using task2.Infrastructure.Services.Abstration;

namespace task2.BLL.Handlers;

public class RateABookCommandHandler : IRequestHandler<RateABookCommand>
{
    private readonly IValidator<RateABookCommand> _validator;
    private readonly IBookService _bookService;
    private readonly IMapper _mapper;

    public RateABookCommandHandler(IValidator<RateABookCommand> validator, IBookService bookService, IMapper mapper)
    {
        _validator = validator;
        _bookService = bookService;
        _mapper = mapper;
    }

    public async Task Handle(RateABookCommand request, CancellationToken cancellationToken)
    {
        var result = await _validator.ValidateAsync(request, cancellationToken);
        if (result.IsValid == false)
        {
            throw new InvalidDataException(string.Join("\n", result.Errors.Select(x => x.ErrorMessage)));
        }

        var dto = _mapper.Map<NewScoreDto>(request);
        await _bookService.RateABook(dto);
    }
}
