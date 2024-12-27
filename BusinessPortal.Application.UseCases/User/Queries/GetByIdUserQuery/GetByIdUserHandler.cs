using AutoMapper;
using BusinessPortal.Application.Dto;
using BusinessPortal.Application.Interface.Persistence;
using BusinessPortal.Application.UseCases.Commons.Bases;
using BusinessPortal.Domain.Entities;
using MediatR;

namespace BusinessPortal.Application.UseCases.Users.Queries.GetByIdUserQuery
{
    public class GetByIdUserHandler : IRequestHandler<GetByIdUserQuery, BaseResponse<UserDto>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetByIdUserHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<BaseResponse<UserDto>> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<UserDto>();
            try
            {
                var User = await _unitOfWork.GetReadRepository<User>().GetAsync(request.UserId);
                if (User is not null)
                {
                    response.Data = _mapper.Map<UserDto>(User);
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
