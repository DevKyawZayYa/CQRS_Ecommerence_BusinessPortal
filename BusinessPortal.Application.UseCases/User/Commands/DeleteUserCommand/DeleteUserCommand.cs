using BusinessPortal.Application.UseCases.Commons.Bases;
using MediatR;

namespace BusinessPortal.Application.UseCases.Users.Commands.DeleteUserCommand
{
    public class DeleteUserCommand : IRequest<BaseResponse<bool>>
    {
        public Guid UserId { get; set; }

    }
}
