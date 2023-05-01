using Microsoft.AspNetCore.Http;
using ShopApp.Domain.DTOs.Image;
using ShopApp.Domain.Entities;

namespace ShopApp.Application.Interfaces.Images
{
    public interface IImageService
    {
        Task<GetImageResponseDto> GetById(int id);
        Task<GetImageResponseDto> GetImageByProduct(int id);
        Task<List<GetImageResponseDto>> GetImagesByProduct(int id);
        Task<GetImageResponseDto> GetImageByUser(int id);
        Task<List<GetImageResponseDto>> GetImagesByProductIdsList(List<int> productIdsList);
        Task<GetImageResponseDto> Add(Image newImage, IFormFile imageFile);
        Task<GetImageResponseDto> Delete(int id);
        Task<GetImageResponseDto> Update(int id, AddImageRequestDto newImage);
    }
}
