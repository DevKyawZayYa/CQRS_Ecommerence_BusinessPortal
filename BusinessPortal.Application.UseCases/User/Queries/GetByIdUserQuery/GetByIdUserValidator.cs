using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessPortal.Application.UseCases.Users.Queries.GetByIdUserQuery
{
    public class GetByIdUserValidator : AbstractValidator<GetByIdUserQuery>
    {
        public GetByIdUserValidator()
        {
            
        }
    }
}
