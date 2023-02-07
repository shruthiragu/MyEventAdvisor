using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderApi.Models
{
    public class OrderItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
        public int ProductId { get; private set; }
        public decimal UnitPrice { get; set; }
        public int Units { get; set; }
        //public virtual Order order { get; set; }
        public int OrderId { get; set; } //Foreign key associated with the order

        public OrderItem (string productName, string pictureUrl, int productId, decimal unitPrice, int units)
        {           
            ProductName = productName;
            PictureUrl = pictureUrl;
            ProductId = productId;
            UnitPrice = unitPrice;
            Units = units;            
        }

        public void SetPictureUrl(string pictureUrl)
        {
            if (!String.IsNullOrEmpty(pictureUrl))
            {
                PictureUrl = pictureUrl;
            }
        }

        public void AddUnits(int units)
        {
            Units += units;
        }
    }
}
