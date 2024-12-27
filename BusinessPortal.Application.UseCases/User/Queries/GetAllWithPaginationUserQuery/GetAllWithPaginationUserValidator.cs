using FluentValidation;

namespace BusinessPortal.Application.UseCases.Users.Queries.GetAllWithPaginationUserQuery
{
    public class GetAllWithPaginationUserValidator : AbstractValidator<GetAllWithPaginationUserQuery>
    {
        public GetAllWithPaginationUserValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1)
                .NotNull()
                .NotEmpty();
            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1)
                .NotNull()
                .NotEmpty();
        }
    }
}
