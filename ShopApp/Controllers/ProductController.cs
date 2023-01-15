using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            return Ok(await _productService.GetAllProducts());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Product>> GetProduct([FromRoute] int id)
        {
            var product = await _productService.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct([FromBody] Product newProduct)
        {
            return Ok(await _productService.AddProduct(newProduct));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct([FromRoute] int id)
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
        public async Task<ActionResult<Product>> UpdateProduct([FromRoute] int id, Product newProduct)
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
