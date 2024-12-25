using BusinessPortal.Application.Dto;
using BusinessPortal.Application.UseCases.Commons.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessPortal.Application.UseCases.Customers.Queries.GetAllWithPaginationCustomerQuery
{
    public class GetAllWithPaginationCustomerQuery : IRequest<BaseResponsePagination<IEnumerable<CustomerDto>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
