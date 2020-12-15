using Online_Shopping.DomainModel;


namespace Online_Shopping.ViewModel
{
    public class OrderDetailViewModel
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int TotalPrice { get; set; }
    }
}
