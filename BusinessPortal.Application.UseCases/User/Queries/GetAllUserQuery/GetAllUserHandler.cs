using AutoMapper;
using BusinessPortal.Application.Dto;
using BusinessPortal.Application.Interface.Persistence;
using BusinessPortal.Application.UseCases.Commons.Bases;
using BusinessPortal.Domain.Entities;
using MediatR;

namespace BusinessPortal.Application.UseCases.Users.Queries.GetAllUserQuery
{
    public class GetAllUserHandler : IRequestHandler<GetAllUserQuery, BaseResponse<IEnumerable<UserDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllUserHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper));
        }

        public async Task<BaseResponse<IEnumerable<UserDto>>>Handle(GetAllUserQuery request, CancellationToken cancellation)
        {
            var response    = new BaseResponse<IEnumerable<UserDto>>();

            try
            {
                var Users = await _unitOfWork.GetReadRepository<User>().GetAllAsync();

                if(Users is not null)
                {
                    response.Data = _mapper.Map<IEnumerable<UserDto>>(Users);
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
