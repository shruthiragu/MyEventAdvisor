using System;
using WebMvc.Models.OrderModels;

namespace WebMvc.Services
{
	public interface IOrderService
    {
        Task<int> CreateOrder(Order order);
        Task<Order> GetOrder(string orderId);
    }
}

