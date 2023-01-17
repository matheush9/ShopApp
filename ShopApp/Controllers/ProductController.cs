using Microsoft.AspNetCore.Mvc;
using ShopApp.Dtos.Product;

namespace ShopApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetProductResponseDto>>> GetProducts()
        {
            return Ok(await _productService.GetAllProducts());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<GetProductResponseDto>> GetProduct([FromRoute] int id)
        {
            var product = await _productService.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<GetProductResponseDto>> AddProduct([FromBody] AddProductRequestDto newProduct)
        {
            return Ok(await _productService.AddProduct(newProduct));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<GetProductResponseDto>> DeleteProduct([FromRoute] int id)
        {
            var product = await _productService.DeleteProduct(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<GetProductResponseDto>> UpdateProduct([FromRoute] int id, AddProductRequestDto newProduct)
        {
            var product = await _productService.UpdateProduct(id, newProduct);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }
    }
}
