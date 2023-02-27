using FluentValidation;
using task2.BLL.Commands;
using task2.Infrastructure.Dtos;

namespace task2.BLL.Validators;

public class SaveReviewCommandValidator: AbstractValidator<SaveReviewCommand>
{
    public SaveReviewCommandValidator()
    {
        RuleFor(x => x.Reviewer).Length(1, 100).WithMessage("Reviwer name's length has to be greater than 100");
        RuleFor(x => x.Message).Length(1, 100).WithMessage("Reviwer name's length has to be greater than 100");
        RuleFor(x => x.BookId).GreaterThan(0).WithMessage("Book id has to be greater than 0");
    }
}
