using Online_Shopping.DomainModel;


namespace Online_Shopping.ViewModel
{
    public class CartViewModel
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string Username { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int ProductId { get; set; }
        public decimal TotalPrice { get; set; }
        public string ProductName { get; set; }
        
        public virtual Product Product { get; set; }
    }
}
