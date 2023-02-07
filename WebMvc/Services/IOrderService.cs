using WebMvc.Models.OrderModels;

namespace WebMvc.Services
{
    public interface IOrderService
    {
        Task<Order> GetOrder(int orderId);
        Task<int> CreateOrder(Order order);
    }
}
