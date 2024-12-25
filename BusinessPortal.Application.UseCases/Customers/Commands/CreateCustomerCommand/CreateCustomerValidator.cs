using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessPortal.Application.UseCases.Customers.Commands.CreateCustomerCommand
{
    public class CreateCustomerValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerValidator()
        {

            RuleFor(x => x.Phone).NotEmpty().NotNull();
            RuleFor(x => x.Region).NotEmpty().NotNull();
        }
    }
}
