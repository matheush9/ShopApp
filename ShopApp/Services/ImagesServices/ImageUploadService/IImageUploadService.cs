namespace ShopApp.Services.ImagesServices.ImageUploadService
{
    public interface IImageUploadService
    {
        Task<string> UploadImage(IFormFile image, bool smallImage);
        Task<byte[]> ResizeImage(byte[] imageData, int maxWidth, int maxHeight);
        ImagePaths ConstructImagePath(IFormFile image, bool smallImage);
    }
}
