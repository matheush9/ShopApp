using AutoMapper;
using Microsoft.AspNetCore.Http;
using ShopApp.Application.Interfaces.Images;
using ShopApp.Application.Interfaces.Images.ImageUploadService;
using ShopApp.Domain.DTOs.Image;
using ShopApp.Domain.Entities;
using ShopApp.Infrastructure.Repositories.Abstractions;

namespace ShopApp.Application.Services.Images
{
    public class ImageService : IImageService
    {
        private readonly IMapper _mapper;
        private readonly IImageUploadService _imageUploadService;
        private readonly IImageRepository _imageRepository;

        public ImageService(IMapper mapper, IImageUploadService imageUploadService, IImageRepository imageRepository)
        {
            _mapper = mapper;
            _imageUploadService = imageUploadService;
            _imageRepository = imageRepository;
        }

        public async Task<GetImageResponseDto> GetById(int id)
        {
            var image = await _imageRepository.GetByIdAsync(id);

            return _mapper.Map<GetImageResponseDto>(image);
        }

        public async Task<GetImageResponseDto> GetImageByUser(int id)
        {
            var image = await _imageRepository.GetImageByUser(id);

            return _mapper.Map<GetImageResponseDto>(image);
        }

        public async Task<GetImageResponseDto> Add(Image newImage, IFormFile imageFile)
        {
            newImage.SmallImagePath = await _imageUploadService.UploadImage(imageFile, true);
            newImage.LargeImagePath = await _imageUploadService.UploadImage(imageFile, false);

            await _imageRepository.AddAsync(newImage);

            return _mapper.Map<GetImageResponseDto>(newImage);
        }

        public async Task<GetImageResponseDto> Delete(int id)
        {
            var image = await _imageRepository.GetByIdAsync(id);

            if (image != null)
                await _imageRepository.DeleteAsync(image);

            return _mapper.Map<GetImageResponseDto>(image);
        }

        public async Task<GetImageResponseDto> Update(int id, AddImageRequestDto newImage)
        {
            var image = await _imageRepository.GetByIdAsync(id);

            if (image != null)
            {
                image.Name = newImage.Name;

                await _imageRepository.UpdateAsync(image);
            }

            return _mapper.Map<GetImageResponseDto>(image);
        }
    }
}
