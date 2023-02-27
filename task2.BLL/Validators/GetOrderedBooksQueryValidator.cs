using FluentValidation;
using task2.BLL.Queries;
using task2.Infrastructure.Dtos;

namespace task2.BLL.Validators;

public class GetOrderedBooksQueryValidator : AbstractValidator<GetOrderedBooksQuery>
{
   
    public GetOrderedBooksQueryValidator()
    {
        RuleFor(x => x.OrderPropName).Must(x => x == nameof(BookDto.Author) || x == nameof(BookDto.Title))
            .WithMessage("The allowed order prop name are: author or title!");
    }
}
