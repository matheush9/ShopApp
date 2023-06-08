using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Application.Filters;
using ShopApp.Application.Interfaces.ProductService;
using ShopApp.Domain.Common;
using ShopApp.Domain.DTOs.Image;
using ShopApp.Domain.DTOs.Products;

namespace ShopApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IAuthorizationService _authorizationService;

        public ProductController(IProductService productService, IAuthorizationService authorizationService)
        {
            _productService = productService;
            _authorizationService = authorizationService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetProductResponseDto>> GetById([FromRoute] int id)
        {
            var product = await _productService.GetById(id);

            if (product is null)
                return NotFound(product);

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<GetProductResponseDto>> Add([FromBody] AddProductRequestDto newProduct)
        {
            if (await Authorize(newProduct) is false)
                return Forbid();

            await _productService.Add(newProduct);
            return Ok();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<GetProductResponseDto>> Delete([FromRoute] int id)
        {
            var getProduct = await _productService.GetById(id);

            if (await Authorize(getProduct) is false)
                return Forbid();

            var product = await _productService.Delete(id);

            if (product is null)
                return NotFound(product);

            return Ok(product);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<GetProductResponseDto>> Update([FromRoute] int id, AddProductRequestDto newProduct)
        {
            var getProduct = await _productService.GetById(id);

            if (await Authorize(getProduct) is false)
                return Forbid();

            var product = await _productService.Update(id, newProduct);

            if (product is null)
                return NotFound(product);

            return Ok(product);
        }

        [HttpGet("idList")]
        public async Task<ActionResult<List<GetProductResponseDto>>> GetProductsByIdsList([FromQuery] List<int> proId)
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

        [NonAction]
        public async Task<bool> Authorize(BaseStore baseStore)
        {
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, baseStore, "StorePolicy");

            if (!authorizationResult.Succeeded)
            {
                return false;
            }

            return true;
        }
    }
}