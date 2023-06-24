using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ShopApp.Application.Interfaces.Images;
using ShopApp.Application.Interfaces.Images.ImageUploadService;
using ShopApp.Domain.DTOs.Image;
using ShopApp.Domain.Entities;
using ShopApp.Infrastructure.Data;

namespace ShopApp.Application.Services.Images
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

        public async Task<GetImageResponseDto> GetImageByUser(int id)
        {
            var image = await _context.Images.FirstOrDefaultAsync(i => i.UserId == id);

            return _mapper.Map<GetImageResponseDto>(image);
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
