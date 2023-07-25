using AutoMapper;
using ShopApp.Domain.DTOs.Image;
using ShopApp.Domain.Entities;

namespace ShopApp.Domain.Mapper
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
