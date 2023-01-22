using ShopApp.Dtos.Product;

namespace ShopApp.Services.ResponseHandlers
{
    public class ProductResponseHandler: IResponseHandler<GetProductResponseDto>
    {
        public ServiceResponse<GetProductResponseDto> SetResponse(ServiceResponse<GetProductResponseDto> response)
        {
            NullResponse(response);
            return response;
        }
        public ServiceResponse<GetProductResponseDto> NullResponse(ServiceResponse<GetProductResponseDto> response)
        {
            if (response.Data == null)
            {
                response.Message = "Product was not found";
                response.Success = false;
            }
            
            return response;
        }
    }
}
