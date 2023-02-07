using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OrderAPI.Models

{
    public class OrderItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string EventName { get; set; }
        public string PictureUrl { get; set; }
        public decimal UnitPrice { get; set; }
        public int Units { get; set; }
        public int EventId { get; private set; }

        protected OrderItem() { }

        //public virtual Order Order { get; set; }

        public int OrderId { get; set; }

        public OrderItem(int eventId, string eventName, decimal unitPrice, string pictureUrl, int units = 1)
        {
            
            

            EventId = eventId;
            EventName = eventName;
            UnitPrice = unitPrice;
            Units = units;
            PictureUrl = pictureUrl;
        }

        public void SetPictureUri(string pictureUri)
        {
            if (!String.IsNullOrWhiteSpace(pictureUri))
            {
                PictureUrl = pictureUri;
            }
        }

        public void AddUnits(int units)
        {
            
            

            Units += units;
        }
    }
}
