using Microsoft.AspNetCore.Mvc;
using ShopApp.Dtos.Products;
using ShopApp.Services.Generic;
using ShopApp.Services.ProductService;

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

        [HttpGet("filter")]
        public async Task<ActionResult<List<GetProductResponseDto>>> Filter([FromQuery] ProductQueryParams productParams)
        {
            var products = await _productService.Filter(productParams);

            if (products is null)
                return NotFound(products);

            return Ok(products);
        }

        [HttpPut("sell/{id}")]
        public async Task<ActionResult<List<GetProductResponseDto>>> SellProduct([FromRoute] int id)
        {
            var products = await _productService.SellProduct(id);

            if (products is null)
                return NotFound(products);

            return Ok(products);
        }
    }
}