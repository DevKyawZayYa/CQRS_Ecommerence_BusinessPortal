using BusinessPortal.Application.Dto;
using BusinessPortal.Application.UseCases.Commons.Bases;
using MediatR;

namespace BusinessPortal.Application.UseCases.Users.Queries.GetByIdUserQuery
{
    public class GetByIdUserQuery : IRequest<BaseResponse<UserDto>>
    {
        public Guid UserId { get; set; }
    }
}
