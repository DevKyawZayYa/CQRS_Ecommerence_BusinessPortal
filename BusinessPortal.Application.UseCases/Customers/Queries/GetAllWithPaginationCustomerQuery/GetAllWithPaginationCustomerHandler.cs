using AutoMapper;
using BusinessPortal.Application.Dto;
using BusinessPortal.Application.Interface.Persistence;
using BusinessPortal.Application.UseCases.Commons.Bases;
using MediatR;

namespace BusinessPortal.Application.UseCases.Customers.Queries.GetAllWithPaginationCustomerQuery
{
    public class GetAllWithPaginationCustomerHandler : IRequestHandler<GetAllWithPaginationCustomerQuery, BaseResponsePagination<IEnumerable<CustomerDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllWithPaginationCustomerHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<BaseResponsePagination<IEnumerable<CustomerDto>>> Handle(GetAllWithPaginationCustomerQuery request, CancellationToken cancellationToken)
        {
            var response = new BaseResponsePagination<IEnumerable<CustomerDto>>();
            try
            {
                var count = await _unitOfWork.Customers.CountAsync();

                var customers = await _unitOfWork.Customers.GetAllWithPaginationAsync(request.PageNumber, request.PageSize);

                if (customers is not null)
                {
                    response.PageNumber = request.PageNumber;
                    response.TotalPages = (int)Math.Ceiling(count / (double)request.PageSize);
                    response.TotalCount = count;
                    response.Data = _mapper.Map<IEnumerable<CustomerDto>>(customers);
                    response.succcess = true;
                    response.Message = "Query succeed!";
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
