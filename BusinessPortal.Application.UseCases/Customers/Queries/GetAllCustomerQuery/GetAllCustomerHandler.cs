using AutoMapper;
using BusinessPortal.Application.Dto;
using BusinessPortal.Application.Interface.Persistence;
using BusinessPortal.Application.UseCases.Commons.Bases;
using BusinessPortal.Domain.Entities;
using MediatR;

namespace BusinessPortal.Application.UseCases.Customers.Queries.GetAllCustomerQuery
{
    public class GetAllCustomerHandler : IRequestHandler<GetAllCustomerQuery, BaseResponse<IEnumerable<CustomerDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllCustomerHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper));
        }

        public async Task<BaseResponse<IEnumerable<CustomerDto>>>Handle(GetAllCustomerQuery request, CancellationToken cancellation)
        {
            var response    = new BaseResponse<IEnumerable<CustomerDto>>();

            try
            {
                var customers = await _unitOfWork.GetReadRepository<Customer>().GetAllAsync();

                if(customers is not null)
                {
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
