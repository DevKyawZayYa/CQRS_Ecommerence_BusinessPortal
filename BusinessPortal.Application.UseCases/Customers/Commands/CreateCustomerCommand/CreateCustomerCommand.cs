using BusinessPortal.Application.UseCases.Commons.Bases;
using MediatR;

namespace BusinessPortal.Application.UseCases.Customers.Commands.CreateCustomerCommand
{
    public class CreateCustomerCommand : IRequest<BaseResponse<bool>>
    {
        public Guid CustomerId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public string? Role { get; set; }
    }
}
