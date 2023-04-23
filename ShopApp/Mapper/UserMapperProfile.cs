using AutoMapper;
using ShopApp.Dtos.User;

namespace ShopApp.Mapper
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<User, GetUserResponseDto>();
            CreateMap<User, AddUserRequestDto>();
            CreateMap<AddUserRequestDto, User>();
        }
    }
}
