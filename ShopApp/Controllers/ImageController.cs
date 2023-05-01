using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopApp.Application.Interfaces.Images;
using ShopApp.Domain.DTOs.Image;
using ShopApp.Domain.Entities;

namespace ShopApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : Controller
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var image = await _imageService.GetById(id);

            if (image is null)
                return NotFound(image);

            return Ok(image);
        }

        [HttpGet("product/list/{id}")]
        public async Task<IActionResult> GetImagesByProduct([FromRoute] int id)
        {
            var image = await _imageService.GetImagesByProduct(id);

            if (image is null)
                return NotFound(image);

            return Ok(image);
        }

        [HttpGet("product/{id}")]
        public async Task<IActionResult> GetImageByProductId([FromRoute] int id)
        {
            var image = await _imageService.GetImageByProduct(id);

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

        [HttpGet("products/list")]
        public async Task<ActionResult<List<GetImageResponseDto>>> GetImagesByProductIdsList([FromQuery] List<int> proId)
        {
            var products = await _imageService.GetImagesByProductIdsList(proId);

            if (products is null)
                return NotFound(products);

            return Ok(products);
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
            var image = await _imageService.Delete(id);

            if (image is null)
                return NotFound(image);

            return Ok(image);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, AddImageRequestDto newImage)
        {
            var image = await _imageService.Update(id, newImage);

            if (image is null)
                return NotFound(image);

            return Ok(image);
        }
    }
}
