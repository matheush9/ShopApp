namespace ShopApp.Models
{
    public class Cart
    {
        public int Id { get; set; } 
        public List<Item> Items { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }      
    }
}
