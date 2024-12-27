using AutoMapper;
using BusinessPortal.Application.Dto;
using BusinessPortal.Application.Interface.Persistence;
using BusinessPortal.Application.UseCases.Commons.Bases;
using BusinessPortal.Domain.Entities;
using MediatR;

namespace BusinessPortal.Application.UseCases.Users.Queries.GetAllWithPaginationUserQuery
{
    public class GetAllWithPaginationUserHandler : IRequestHandler<GetAllWithPaginationUserQuery, BaseResponsePagination<IEnumerable<UserDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllWithPaginationUserHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<BaseResponsePagination<IEnumerable<UserDto>>> Handle(GetAllWithPaginationUserQuery request, CancellationToken cancellationToken)
        {
            var response = new BaseResponsePagination<IEnumerable<UserDto>>();
            try
            {
                var count = await _unitOfWork.GetReadRepository<User>().CountAsync();

                var Users = await _unitOfWork.GetReadRepository<User>().GetAllWithPaginationAsync(request.PageNumber, request.PageSize);

                if (Users is not null)
                {
                    response.PageNumber = request.PageNumber;
                    response.TotalPages = (int)Math.Ceiling(count / (double)request.PageSize);
                    response.TotalCount = count;
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
