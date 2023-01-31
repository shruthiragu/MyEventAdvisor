namespace WebMvc.Models.OrderModels
{
    public class OrderItem
    {
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Units { get; set; }
    }
}
