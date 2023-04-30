namespace ShopApp.Domain.DTOs.Store
{
    public class AddStoreRequestDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Country { get; set; }
        public int UserId { get; set; }
    }
}
