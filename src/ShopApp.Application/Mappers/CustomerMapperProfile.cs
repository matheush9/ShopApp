using AutoMapper;
using ShopApp.Domain.DTOs.Customer;
using ShopApp.Domain.Entities;

namespace ShopApp.Domain.Mapper
{
    public class CustomerMapperProfile : Profile
    {
        public CustomerMapperProfile()
        {
            CreateMap<Customer, GetCustomerResponseDto>();
            CreateMap<Customer, AddCustomerRequestDto>();
            CreateMap<AddCustomerRequestDto, Customer>();
        }
    }
}
