using ShopApp.Dtos.Image;

namespace ShopApp.Services.ImagesServices
{
    public interface IImageService
    {
        Task<GetImageResponseDto> GetById(int id);
        Task<GetImageResponseDto> GetImageByProduct(int id);
        Task<List<GetImageResponseDto>> GetImagesByProduct(int id);
        Task<List<GetImageResponseDto>> GetImagesByProductIdsList(List<int> productIdsList);
        Task<GetImageResponseDto> Add(Image newImage, IFormFile imageFile);
        Task<GetImageResponseDto> Delete(int id);
        Task<GetImageResponseDto> Update(int id, AddImageRequestDto newImage);
    }
}
