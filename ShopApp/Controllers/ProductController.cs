using Microsoft.AspNetCore.Mvc;
using ShopApp.Dtos.Products;
using ShopApp.Services.GenericService;

namespace ShopApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : GenericController<GetProductResponseDto, AddProductRequestDto>
    {
        public ProductController(IGenericService<GetProductResponseDto, AddProductRequestDto> genericService) : base(genericService)
        {

        }
    }
}