namespace ShopApp.Application.Interfaces.Generic
{
    public interface IGenericService<T, T2>
    {
        Task<T> GetById(int id);
        Task Add(T2 requestEntity);
        Task<T> Delete(int id);
        Task<T> Update(int id, T2 requestEntity);
    }
}
