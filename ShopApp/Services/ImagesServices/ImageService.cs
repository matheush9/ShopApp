using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopApp.Data;
using ShopApp.Dtos.Image;
using ShopApp.Services.ImagesServices.ImageUploadService;

namespace ShopApp.Services.ImagesServices
{
    public class ImageService : IImageService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IImageUploadService _imageUploadService;

        public ImageService(DataContext context, IMapper mapper, IImageUploadService imageUploadService)
        {
            _mapper = mapper;
            _context = context;
            _imageUploadService = imageUploadService;
        }

        public async Task<GetImageResponseDto> GetById(int id)
        {
            var image = await _context.Images.FirstOrDefaultAsync(i => i.Id == id);

            return _mapper.Map<GetImageResponseDto>(image);
        }

        public async Task<List<GetImageResponseDto>> GetImagesByProduct(int id)
        {
            var images = await _context.Images.Where(p => p.ProductId == id).ToListAsync();

            return _mapper.Map<List<GetImageResponseDto>>(images);
        }

        public async Task<GetImageResponseDto> GetImageByProduct(int id)
        {
            var image = await _context.Images.FirstOrDefaultAsync(p => p.ProductId == id);

            return _mapper.Map<GetImageResponseDto>(image);
        }

        public async Task<List<GetImageResponseDto>> GetImagesByProductIdsList(List<int> productIdsList)
        {
            var images = new List<Image?>();

            if (productIdsList != null && productIdsList.Count > 0)
            {

                images = await _context.Images.Where(i => i.ProductId != null && productIdsList.Contains(i.ProductId.Value))
                                    .GroupBy(i => i.ProductId ?? 0)
                                    .Select(g => g.FirstOrDefault()).ToListAsync();
            }

            return _mapper.Map<List<GetImageResponseDto>>(images);
        }

        public async Task<GetImageResponseDto> Add(Image newImage, IFormFile imageFile)
        {
            newImage.SmallImagePath = await _imageUploadService.UploadImage(imageFile, true);
            newImage.LargeImagePath = await _imageUploadService.UploadImage(imageFile, false);

            _context.Images.Add(newImage);
            await _context.SaveChangesAsync();

            return _mapper.Map<GetImageResponseDto>(newImage);
        }

        public async Task<GetImageResponseDto> Delete(int id)
        {
            var image = await _context.Images.FindAsync(id);

            if (image != null)
            {
                _context.Images.Remove(image);
                await _context.SaveChangesAsync();
            }

            return _mapper.Map<GetImageResponseDto>(image);
        }

        public async Task<GetImageResponseDto> Update(int id, AddImageRequestDto newImage)
        {
            var image = await _context.Images.FindAsync(id);

            if (image != null)
            {
                image.Name = newImage.Name;

                await _context.SaveChangesAsync();
            }

            return _mapper.Map<GetImageResponseDto>(image);
        }
    }
}
