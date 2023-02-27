using FluentValidation;
using task2.BLL.Commands;
using task2.Infrastructure.Dtos;

namespace task2.BLL.Validators;

public class SaveBookCommandValidator: AbstractValidator<SaveBookCommand>
{
    public SaveBookCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).Unless(x => x.Id == null).WithMessage("Book id has to be greater than 0");
        RuleFor(x => x.Cover).NotEmpty().MinimumLength(1);
        RuleFor(x => x.Content).NotEmpty().MinimumLength(2);
        RuleFor(x => x.Author).NotEmpty().MinimumLength(1);
    }
}
