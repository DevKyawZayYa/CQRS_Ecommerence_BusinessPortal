using BusinessPortal.Application.Dto;
using BusinessPortal.Application.UseCases.Commons.Bases;
using MediatR;

namespace BusinessPortal.Application.UseCases.Customers.Queries.GetByIdCustomerQuery
{
    public class GetByIdCustomerQuery : IRequest<BaseResponse<CustomerDto>>
    {
        public string? CustomerId { get; set; }
    }
}
