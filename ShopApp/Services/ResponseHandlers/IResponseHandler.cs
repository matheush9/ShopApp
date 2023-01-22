namespace ShopApp.Services.ResponseHandlers
{
    public interface IResponseHandler<T>
    {
        ServiceResponse<T> SetResponse(ServiceResponse<T> response);
        ServiceResponse<T> NullResponse(ServiceResponse<T> response);
    }
}
