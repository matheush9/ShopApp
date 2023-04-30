namespace ShopApp.Application.ResponseWrappers
{
    public class BaseResponse<T>
    {
        public BaseResponse()
        {
        }
        public BaseResponse(T data)
        {
            Succeeded = true;
            Message = string.Empty;
            Errors = null;
            Data = data;
        }
        public T Data { get; set; }
        public bool Succeeded { get; set; }
        public string[] Errors { get; set; }
        public string Message { get; set; }
    }
}
