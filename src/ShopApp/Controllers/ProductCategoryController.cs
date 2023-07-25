using Microsoft.AspNetCore.Mvc;
using ShopApp.Application.DTOs.ProductCategory;
using ShopApp.Application.Interfaces.ProductCategory;
using ShopApp.Domain.DTOs.Category;

namespace ShopApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategoryService _productCategoryService;

        public ProductCategoryController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetProductCategoryResponseDto>> GetProductCategoryById([FromRoute] int id)
        {
            var productCategory = await _productCategoryService.GetProductCategoryById(id);

            if (productCategory is null)
                return NotFound(productCategory);

            return Ok(productCategory);
        }

        [HttpGet]
        public async Task<ActionResult<List<GetProductCategoryResponseDto>>> GetAllProductCategories()
        {
            var categories = await _productCategoryService.GetAllProductCategories();

            if (categories is null)
                return NotFound(categories);

            return Ok(categories);
        }

        [HttpPost]
        public async Task<ActionResult<GetProductCategoryResponseDto>> AddCategory(AddProductCategoryRequestDTO newCategory)
        {            
            return Ok(await _productCategoryService.AddProductCategory(newCategory));
        }
    }
}
