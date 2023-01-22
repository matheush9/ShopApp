namespace ShopApp.Services.ResponseHandlers
{
    public class DefaultResponseHandler<T>
    {
        public ServiceResponse<T> SetResponse(ServiceResponse<T> response)
        {
            if (response.Data is null)
            {
                response.Message = "The requested resource was not found";
                response.Success = false;
            }

            return response;
        }
    }
}
