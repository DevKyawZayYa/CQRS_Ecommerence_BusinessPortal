using MediatR;
using BusinessPortal.Application.UseCases.Commons.Bases;
using BusinessPortal.Application.Dto;

namespace BusinessPortal.Application.UseCases.Users.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest<BaseResponse<UserDto>>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? MobileCode { get; set; }
        public string? MobileNumber { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
    }
}
