using FluentValidation;
using task2.BLL.Commands;

namespace task2.BLL.Validators;

public class DeleteBookCommandValidator:AbstractValidator<DeleteBookCommand>
{
    public DeleteBookCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id has to be greater than 0");
    }
}
