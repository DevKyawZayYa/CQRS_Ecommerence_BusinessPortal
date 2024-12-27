using AutoMapper;
using BCrypt.Net;
using BusinessPortal.Application.Dto;
using BusinessPortal.Application.Interface.Persistence;
using BusinessPortal.Application.UseCases.Commons.Bases;
using BusinessPortal.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BusinessPortal.Application.UseCases.Users.Commands.RegisterUser
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, BaseResponse<UserDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<RegisterUserHandler> _logger;
        private readonly IValidator<RegisterUserCommand> _validator;

        public RegisterUserHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<RegisterUserHandler> logger,IValidator<RegisterUserCommand>validator)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<BaseResponse<UserDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<UserDto>();

            // Validate the request
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
            {
                response.Message = "Validation failed";
                response.Errors = validationResult.Errors.Select(e => new BaseError { ErrorMessage = e.ErrorMessage }).ToList();
                return response;
            }

            try
            {
               var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password, workFactor: 12);

                var user = new User
                {
                    UserId = Guid.NewGuid(),
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    Password = hashedPassword,
                    MobileCode = request.MobileCode,
                    MobileNumber = request.MobileNumber,
                    Address = request.Address,
                    City = request.City,
                    Region = request.Region,
                    PostalCode = request.PostalCode,
                    Country = request.Country,
                    CreatedDate = DateTime.UtcNow,
                    IsActive = true
                };

                // Insert user into the database
                var result = await _unitOfWork.GetWriteRepository<User>().InsertAsync(user);
                await _unitOfWork.CompleteAsync();

                if (result)
                {
                    response.Data = _mapper.Map<UserDto>(user);
                    response.succcess = true;
                    response.Message = "User registered successfully!";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while registering the user.");
                response.Message = "An error occurred during registration. Please try again later.";
            }

            return response;
        }
    }
}
