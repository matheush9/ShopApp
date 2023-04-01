using Microsoft.AspNetCore.Mvc;
using ShopApp.Dtos.Products;
using ShopApp.Services.GenericService;
using ShopApp.Services.ProductServices;

namespace ShopApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : GenericController<GetProductResponseDto, AddProductRequestDto>
    {
        private readonly IProductService _productService;

        public ProductController(IGenericService<GetProductResponseDto, AddProductRequestDto> genericService, IProductService productService) : base(genericService)
        {
            _productService = productService;
        }

        [HttpGet("search/{query}")]
        public async Task<ActionResult<List<GetProductResponseDto>>> SearchProductByName(string query)
        {
            var products = await _productService.SearchProduct(query);

            if (products is null)
                return NotFound(products);

            return Ok(products);
        }
    }
}