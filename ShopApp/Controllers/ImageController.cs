using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopApp.Application.Interfaces.Images;
using ShopApp.Application.Interfaces.ProductService;
using ShopApp.Domain.DTOs.Image;
using ShopApp.Domain.Entities;

namespace ShopApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;
        private readonly IAuthorizationService _authorizationService;
        private readonly IProductService _productService;

        public ImageController(IImageService imageService, IAuthorizationService authorizationService, IProductService productService)
        {
            _imageService = imageService;
            _authorizationService = authorizationService;
            _productService = productService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var image = await _imageService.GetById(id);

            if (image is null)
                return NotFound(image);

            return Ok(image);
        }

        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetImageByUserId([FromRoute] int id)
        {
            var image = await _imageService.GetImageByUser(id);

            if (image is null)
                return NotFound(image);

            return Ok(image);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] string json, [FromForm] IFormFile imageFile)
        {
            Image? newImage = JsonConvert.DeserializeObject<Image>(json);

            if (newImage is null)
                return BadRequest(newImage);

            if (newImage.ProductId == null && newImage.UserId == null)
                return BadRequest("The image must have a product or user associated.");

            return Ok(await _imageService.Add(newImage, imageFile));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var getImage = await _imageService.GetById(id);

            if (await Authorize(getImage) is false)
                return Forbid();

            var image = await _imageService.Delete(id);

            if (image is null)
                return NotFound(image);

            return Ok(image);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, AddImageRequestDto newImage)
        {
            var getImage = await _imageService.GetById(id);

            if (await Authorize(getImage) is false)
                return Forbid();

            var image = await _imageService.Update(id, newImage);

            if (image is null)
                return NotFound(image);

            return Ok(image);
        }

        [NonAction]
        public async Task<bool> Authorize(GetImageResponseDto image)
        {
            AuthorizationResult authorizationResult;

            if (image.ProductId == 0)
            {
                authorizationResult = await _authorizationService.AuthorizeAsync(User, image, "UserPolicy");
            }
            else
            {
                var product = await _productService.GetById(image.ProductId);
                authorizationResult = await _authorizationService.AuthorizeAsync(User, product, "StorePolicy");
            }
             
            if (!authorizationResult.Succeeded)
            {
                return false;
            }

            return true;
        }
    }
}
