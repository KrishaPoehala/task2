using FluentValidation;
using task2.BLL.Queries;

namespace task2.BLL.Validators;

public class GetDetailsQueryValidator:AbstractValidator<GetDetailsQuery>
{
    public GetDetailsQueryValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("Book id has to be greater than 0");
    }
}
