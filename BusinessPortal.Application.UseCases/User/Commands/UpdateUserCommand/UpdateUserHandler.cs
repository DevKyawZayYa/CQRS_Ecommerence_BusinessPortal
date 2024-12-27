using AutoMapper;
using BusinessPortal.Application.Interface.Persistence;
using BusinessPortal.Application.UseCases.Commons.Bases;
using BusinessPortal.Domain.Entities;
using MediatR;

namespace BusinessPortal.Application.UseCases.Users.Commands.UpdateUserCommand
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateUserHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<BaseResponse<bool>> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var User = _mapper.Map<User>(command);
                response.Data = await _unitOfWork.GetWriteRepository<User>().UpdateAsync(User);
                if (response.Data)
                {
                    response.succcess = true;
                    response.Message = "Update succeed!";
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
