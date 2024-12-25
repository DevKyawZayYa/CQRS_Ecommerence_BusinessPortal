using AutoMapper;
using BusinessPortal.Application.Dto;
using BusinessPortal.Application.UseCases.Customers.Commands.CreateCustomerCommand;
using BusinessPortal.Application.UseCases.Customers.Commands.UpdateCustomerCommand;
using BusinessPortal.Domain.Entities;

namespace BusinessPortal.Application.UseCases.Commons.Mappings
{
    public class CustomerMapper : Profile
    {
        public CustomerMapper()
        {
            // Map Customer to CustomerDto and vice versa
            CreateMap<Customer, CustomerDto>().ReverseMap();

            // Explicit mapping for CreateCustomerCommand to Customer
            CreateMap<CreateCustomerCommand, Customer>()
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => Guid.NewGuid())) // Generate new Guid for CustomerId
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName)) // Map ContactName to FullName
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email)) // Map ContactTitle to Email (adjust as needed)
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.Region, opt => opt.MapFrom(src => src.Region))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => "Customer")) // Default Role to "Customer"
                ;
            // Explicit mapping for UpdateCustomerCommand to Customer
            CreateMap<UpdateCustomerCommand, Customer>()
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId)) // Ensure CustomerId is mapped
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.Region, opt => opt.MapFrom(src => src.Region))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role ?? "Customer")) // Use Role or default to "Customer"
                ;
        }
    }
}
