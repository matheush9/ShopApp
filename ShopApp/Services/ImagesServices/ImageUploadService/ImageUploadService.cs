using ImageMagick;

namespace ShopApp.Services.ImagesServices.ImageUploadService
{
    public class ImageUploadService : IImageUploadService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ImageUploadService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<string> UploadImage(IFormFile file, bool smallImage)
        {
            if (file != null && file.Length > 0)
            {
                var filePath = ConstructImagePath(file, smallImage);

                int imageSize = smallImage ? 300 : 600;

                var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream);
                var resizedImageBytes = await ResizeImage(memoryStream.ToArray(), imageSize, imageSize);

                await File.WriteAllBytesAsync(filePath, resizedImageBytes);

                return filePath;
            }
            return string.Empty;
        }

        public string ConstructImagePath(IFormFile file, bool smallImage)
        {
            string path = string.Empty;

            if (file.FileName.Contains("-product"))
                path = "/Products";
            else if (file.FileName.Contains("-profile"))
                path = "/Profiles";

            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("Invalid file name! Must have the -product or -profile tag.");

            path += smallImage ? "/Small" : "/Large";

            string fullPath = Path.Combine(_webHostEnvironment.WebRootPath, "Images" + path);
            string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            if (!Directory.Exists(fullPath))
                Directory.CreateDirectory(fullPath);

            return Path.Combine(fullPath, filename);
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
