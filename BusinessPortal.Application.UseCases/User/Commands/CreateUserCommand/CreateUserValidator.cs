using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessPortal.Application.UseCases.Users.Commands.CreateUserCommand
{
    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator()
        {

            RuleFor(x => x.MobileCode).NotEmpty().NotNull();
            RuleFor(x => x.MobileNumber).NotEmpty().NotNull();
            RuleFor(x => x.Region).NotEmpty().NotNull();
        }
    }
}
