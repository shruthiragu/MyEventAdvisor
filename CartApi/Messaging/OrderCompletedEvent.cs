//Cart microservive has to subscribe o the "ordercompletedevent" type of message, with its fully qualified name, ie
//OrderApi.Messaging.OrderCOmpletedEvent. But we want to have a generic name.
//Note that namespace can be changed according to our wish. So I've made it Common.Messaging.
namespace Common.Messaging
{
    public class OrderCompletedEvent
    {//Create a blueprint of the type of message that you want to send.
        public string BuyerId { get; set; }
        public OrderCompletedEvent(string buyerId)
        {
            BuyerId = buyerId;
        }
    }
}

