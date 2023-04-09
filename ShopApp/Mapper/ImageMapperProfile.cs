using AutoMapper;
using ShopApp.Dtos.Image;

namespace ShopApp.Mapper
{
    public class ImageMapperProfile: Profile
    {
        public ImageMapperProfile()
        {
            CreateMap<Image, GetImageResponseDto>();
            CreateMap<Image, AddImageRequestDto>();
            CreateMap<AddImageRequestDto, Image>();
        }
    }
}
