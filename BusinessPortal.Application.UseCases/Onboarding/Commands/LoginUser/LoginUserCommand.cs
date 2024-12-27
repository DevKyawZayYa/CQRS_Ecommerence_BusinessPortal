using MediatR;
using BusinessPortal.Application.UseCases.Commons.Bases;
using BusinessPortal.Application.Dto;

namespace BusinessPortal.Application.UseCases.Users.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<BaseResponse<UserDto>>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
