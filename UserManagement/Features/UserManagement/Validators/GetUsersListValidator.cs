using FluentValidation;
using UserManagement.Features.UserManagement.Queries;

namespace UserManagement.Features.UserManagement.Validators;

public class GetUsersListValidator : AbstractValidator<GetUsersListQuery>
{
    public GetUsersListValidator()
    {
        RuleFor(x => x.PageIndex)
            .GreaterThan(0)
            .WithMessage("Page index must be greater than 0.");
        RuleFor(x => x.PageSize)
            .GreaterThan(0)
            .WithMessage("Page size must be greater than 0.");
    }
}
