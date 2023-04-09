namespace ShopApp.Services.ImagesServices.ImageUploadService
{
    public interface IImageUploadService
    {
        Task<string> UploadImage(IFormFile file, bool smallImage);
        Task<byte[]> ResizeImage(byte[] imageData, int maxWidth, int maxHeight);
        string ConstructImagePath(IFormFile file, bool smallImage);
    }
}
