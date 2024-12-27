using BusinessPortal.Application.Dto;
using BusinessPortal.Application.UseCases.Commons.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessPortal.Application.UseCases.Users.Queries.GetAllWithPaginationUserQuery
{
    public class GetAllWithPaginationUserQuery : IRequest<BaseResponsePagination<IEnumerable<UserDto>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
