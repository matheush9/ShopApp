using AutoMapper;
using ShopApp.Domain.DTOs.User;
using ShopApp.Domain.Entities;

namespace ShopApp.Domain.Mapper
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<User, GetUserResponseDto>();
            CreateMap<User, AddUserRequestDto>();
            CreateMap<AddUserRequestDto, User>();
            CreateMap<EditUserRequestDto, User>();
        }
    }
}
