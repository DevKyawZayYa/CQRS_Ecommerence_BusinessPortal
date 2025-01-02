using AutoMapper;
using BusinessPortal.Application.Dto;
using BusinessPortal.Application.Interface.Persistence;
using BusinessPortal.Application.UseCases.Commons.Bases;
using BusinessPortal.Domain.Entities;
using MediatR;
using BusinessPortal.Application.UseCases.Services;

namespace BusinessPortal.Application.UseCases.Users.Commands.LoginUser
{
    public class LoginUserHandler : IRequestHandler<LoginUserCommand, BaseResponse<UserDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly JwtTokenService _jwtTokenService;

        public LoginUserHandler(IUnitOfWork unitOfWork, IMapper mapper, JwtTokenService jwtTokenService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _jwtTokenService = jwtTokenService ?? throw new ArgumentNullException(nameof(jwtTokenService));
        }

        public async Task<BaseResponse<UserDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<UserDto>();

            try
            {
                var user = await _unitOfWork.GetReadRepository<User>()
                    .FirstOrDefaultAsync(u => u.Email == request.Email);

                if (user != null && BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                {
                    var userDto = _mapper.Map<UserDto>(user);
                    var token = _jwtTokenService.GenerateToken(user);

                    response.Data = userDto;
                    response.succcess = true;
                    response.Message = "Login successful!";
                    response.Token = token; // Add the token to the response
                }
                else
                {
                    response.Message = "Invalid email or password.";
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
