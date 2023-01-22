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
        public async Task<ActionResult<ServiceResponse<List<GetProductResponseDto>>>> GetProducts()
        {
            var products = await _productService.GetAllProducts();

            if (products.Success is false)
                return NotFound(products);

            return Ok(products);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ServiceResponse<GetProductResponseDto>>> GetProduct([FromRoute] int id)
        {
            var product = await _productService.GetProductById(id);

            if (product.Success is false)
                return NotFound(product);

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetProductResponseDto>>> AddProduct([FromBody] AddProductRequestDto newProduct)
        {
            var addedProduct = await _productService.AddProduct(newProduct);

            if (addedProduct.Success is false)
                return NotFound(addedProduct);

            return Ok(addedProduct);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<ServiceResponse<GetProductResponseDto>>> DeleteProduct([FromRoute] int id)
        {
            var product = await _productService.DeleteProduct(id);

            if (product.Success is false)
                return NotFound(product);

            return Ok(product);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<ServiceResponse<GetProductResponseDto>>> UpdateProduct([FromRoute] int id, AddProductRequestDto newProduct)
        {
            var product = await _productService.UpdateProduct(id, newProduct);

            if (product.Success is false)
                return NotFound(product);

            return Ok(product);
        }
    }
}
