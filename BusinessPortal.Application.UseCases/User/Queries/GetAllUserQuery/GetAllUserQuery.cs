using BusinessPortal.Application.Dto;
using BusinessPortal.Application.UseCases.Commons.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessPortal.Application.UseCases.Users.Queries.GetAllUserQuery
{
    public class GetAllUserQuery : IRequest<BaseResponse<IEnumerable<UserDto>>>
    {

    }
}
