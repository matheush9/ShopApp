using ShopApp.Domain.Common;

namespace ShopApp.Domain.DTOs.Products
{
    public class AddProductRequestDto : BaseStore
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }  
        public int ProductCategoryId { get; set; }
    }
}
