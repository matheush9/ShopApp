using ImageMagick;
using Microsoft.AspNetCore.Http;
using ShopApp.Application.Interfaces.Images.ImageUploadService;
using ShopApp.Domain.Models;

namespace ShopApp.Application.Services.Images.ImageUploadService
{
    public class ImageUploadService : IImageUploadService
    {
        public async Task<string> UploadImage(IFormFile image, bool smallImage)
        {
            if (image != null && image.Length > 0)
            {
                var imagePath = ConstructImagePath(image, smallImage);

                int imageSize = smallImage ? 300 : 600;

                var memoryStream = new MemoryStream();
                await image.CopyToAsync(memoryStream);
                var resizedImageBytes = await ResizeImage(memoryStream.ToArray(), imageSize, imageSize);

                await File.WriteAllBytesAsync(imagePath.LocalPath, resizedImageBytes);

                return imagePath.RemotePath;
            }
            return string.Empty;
        }

        public ImagePaths ConstructImagePath(IFormFile image, bool smallImage)
        {
            string path = string.Empty;

            if (image.FileName.Contains("-product"))
                path = "/Products";
            else if (image.FileName.Contains("-user"))
                path = "/Users";

            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("Invalid file name! Must have the -product or -user tag.");

            path += smallImage ? "/Small" : "/Large";

            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images" + path);
            string filename = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);

            string localPath = Path.Combine(fullPath, filename);
            string remotePath = "/Images" + path + "/" + filename;

            return new ImagePaths
            {
                LocalPath = localPath,
                RemotePath = remotePath
            };
        }

        public async Task<byte[]> ResizeImage(byte[] imageData, int maxWidth, int maxHeight)
        {
            var imageStream = new MemoryStream(imageData);
            var image = new MagickImage(imageStream);

            image.Resize(new MagickGeometry(maxWidth, maxHeight)
            {
                IgnoreAspectRatio = false,
                Greater = true
            });

            var outputStream = new MemoryStream();
            await image.WriteAsync(outputStream, MagickFormat.Jpeg);

            return outputStream.ToArray();
        }
    }
}
