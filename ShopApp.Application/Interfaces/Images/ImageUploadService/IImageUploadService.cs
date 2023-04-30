using Microsoft.AspNetCore.Http;
using ShopApp.Domain.Models;

namespace ShopApp.Application.Interfaces.Images.ImageUploadService
{
    public interface IImageUploadService
    {
        Task<string> UploadImage(IFormFile image, bool smallImage);
        Task<byte[]> ResizeImage(byte[] imageData, int maxWidth, int maxHeight);
        ImagePaths ConstructImagePath(IFormFile image, bool smallImage);
    }
}
