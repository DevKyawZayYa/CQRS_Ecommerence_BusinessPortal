using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessPortal.Application.UseCases.Customers.Queries.GetByIdCustomerQuery
{
    public class GetByIdCustomerValidator : AbstractValidator<GetByIdCustomerQuery>
    {
        public GetByIdCustomerValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty()
                .NotNull()
                .MinimumLength(5)
                .MaximumLength(5);
        }
    }
}
