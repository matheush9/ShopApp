namespace ShopApp.Application.ResponseWrappers
{
    public class PagedResponse<T>: BaseResponse<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }

        public PagedResponse(T data, int pageNumber, int pageSize, int totalRecords)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.Data = data;
            this.Message = null;
            this.Succeeded = true;
            this.Errors = null;
            this.TotalPages = (int)(totalRecords / (double)pageSize);
            this.TotalRecords = totalRecords;
        }
    }
}
