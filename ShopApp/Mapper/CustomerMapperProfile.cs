using AutoMapper;
using ShopApp.Dtos.Customer;

namespace ShopApp.Mapper
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
