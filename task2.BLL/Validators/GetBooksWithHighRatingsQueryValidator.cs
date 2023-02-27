using FluentValidation;
using task2.BLL.Queries;

namespace task2.BLL.Validators;

public class GetBooksWithHighRatingsQueryValidator:AbstractValidator<GetBooksWithHighRatingsQuery>
{
    public GetBooksWithHighRatingsQueryValidator()
    {
        RuleFor(x => x.Genre).Length(1, 100).WithMessage("The range of the genre length is from 1 to 100");
    }
}
