namespace ShopApp.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProductById(int id);
        Task<Product> AddProduct(Product newProduct);
        Task<Product> UpdateProduct(int id, Product newProduct);
        Task<Product> DeleteProduct(int id);
    }
}
