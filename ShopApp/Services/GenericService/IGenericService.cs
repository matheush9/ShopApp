namespace ShopApp.Services.GenericService
{
    public interface IGenericService<T, T2>
    {
        Task<ServiceResponse<List<T>>> GetAll();
        Task<ServiceResponse<T>> GetById(int id);
        Task<ServiceResponse<T>> Add(T2 requestEntity);
        Task<ServiceResponse<T>> Delete(int id);
        Task<ServiceResponse<T>> Update(int id, T2 requestEntity);
    }
}
