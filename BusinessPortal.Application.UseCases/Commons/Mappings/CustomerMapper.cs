using AutoMapper;
using BusinessPortal.Application.Dto;
using BusinessPortal.Application.UseCases.Users.Commands.CreateUserCommand;
using BusinessPortal.Application.UseCases.Users.Commands.UpdateUserCommand;
using BusinessPortal.Domain.Entities;

namespace BusinessPortal.Application.UseCases.Commons.Mappings
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            // Map User to UserDto and vice versa
            CreateMap<User, UserDto>().ReverseMap();

            // Explicit mapping for CreateUserCommand to User
            CreateMap<CreateUserCommand, User>()
               // .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => Guid.NewGuid())) 
               // .ForMember<string>(dest => dest.FirstName!, opt => opt.MapFrom<string>(src => src.FullName!)) 
               // .ForMember<string>(dest => dest.Email!, opt => opt.MapFrom<string>(src => src.Email!)) 
               //// .ForMember(dest => dest.MobileNumber, (IMemberConfigurationExpression<CreateUserCommand, User, User.Phone> opt) => opt.MapFrom<string>(src => src.Phone))
               // .ForMember<string>(dest => dest.Address!, opt => opt.MapFrom<string>(src => src.Address!))
               // .ForMember<string>(dest => dest.City!, opt => opt.MapFrom<string>(src => src.City!))
               // .ForMember<string>(dest => dest.Region!, opt => opt.MapFrom<string>(src => src.Region!))
               // .ForMember<string>(dest => dest.PostalCode!, opt => opt.MapFrom<string>(src => src.PostalCode!))
               // .ForMember<string>(dest => dest.Country!, opt => opt.MapFrom<string>(src => src.Country!))
               // .ForMember<string>(dest => dest.Role!, opt => opt.MapFrom(src => "User")) // Default Role to "User"
               // .ForMember<string>(dest => dest.IsActive!, opt => opt.MapFrom(src => "User"))
                ;
            // Explicit mapping for UpdateUserCommand to User
            CreateMap<UpdateUserCommand, User>()
                 .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember<string>(dest => dest.FirstName!, opt => opt.MapFrom<string>(src => src.FullName!))
                .ForMember<string>(dest => dest.Email!, opt => opt.MapFrom<string>(src => src.Email!))
                // .ForMember(dest => dest.MobileNumber, (IMemberConfigurationExpression<CreateUserCommand, User, User.Phone> opt) => opt.MapFrom<string>(src => src.Phone))
                .ForMember<string>(dest => dest.Address!, opt => opt.MapFrom<string>(src => src.Address!))
                .ForMember<string>(dest => dest.City!, opt => opt.MapFrom<string>(src => src.City!))
                .ForMember<string>(dest => dest.Region!, opt => opt.MapFrom<string>(src => src.Region!))
                .ForMember<string>(dest => dest.PostalCode!, opt => opt.MapFrom<string>(src => src.PostalCode!))
                .ForMember<string>(dest => dest.Country!, opt => opt.MapFrom<string>(src => src.Country!))
                .ForMember<string>(dest => dest.Role!, opt => opt.MapFrom(src => "User")) // Default Role to "User"
                ;
        }
    }
}
