using AutoMapper;
using BusinessPortal.Application.Interface.Persistence;
using BusinessPortal.Application.UseCases.Commons.Bases;
using BusinessPortal.Domain.Entities;
using MediatR;

namespace BusinessPortal.Application.UseCases.Users.Commands.CreateUserCommand
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateUserHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<BaseResponse<bool>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var User = _mapper.Map<User>(command);
                await _unitOfWork.GetWriteRepository<User>().InsertAsync(User);
                await _unitOfWork.CompleteAsync();

                response.Data = true;
                response.succcess = true;
                response.Message = "Create succeed!";
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
