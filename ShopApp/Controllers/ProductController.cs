using Microsoft.AspNetCore.Mvc;
using ShopApp.Application.Filters;
using ShopApp.Application.Interfaces.Generic;
using ShopApp.Application.Interfaces.ProductService;
using ShopApp.Domain.DTOs.Image;
using ShopApp.Domain.DTOs.Products;

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

        [HttpGet("idList")]
        public async Task<ActionResult<List<GetImageResponseDto>>> GetProductsByIdsList([FromQuery] List<int> proId)
        {
            var products = await _productService.GetProductsByIdsList(proId);

            if (products is null)
                return NotFound(products);

            return Ok(products);
        }

        [HttpGet("filter")]
        public async Task<ActionResult<List<GetProductResponseDto>>> Filter([FromQuery] ProductQueryParamsResponseDto productParams,
            [FromQuery] PaginationFilter paginationFilter)
        {
            var products = await _productService.Filter(productParams, paginationFilter);

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