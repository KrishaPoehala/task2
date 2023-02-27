using FluentValidation;
using task2.BLL.Commands;

namespace task2.BLL.Validators;

public class RateABookCommandValidator:AbstractValidator<RateABookCommand>
{
    public RateABookCommandValidator()
    {
        RuleFor(x => x.Score).InclusiveBetween(1, 5).WithMessage("Score can be from 1 to 5");
        RuleFor(x => x.BookId).GreaterThan(0);
    }
}
