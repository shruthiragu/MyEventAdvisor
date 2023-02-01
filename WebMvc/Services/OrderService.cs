using System;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebMvc.Infrastructure;
using WebMvc.Models.CartModels;
using WebMvc.Models.OrderModels;

namespace WebMvc.Services
{
	public class OrderService : IOrderService
	{
        private readonly string _baseUrl;
        private readonly IHttpClient _client;
        private readonly IHttpContextAccessor _contextAccessor;
        public OrderService(IConfiguration configuration, IHttpClient client, IHttpContextAccessor contextAccessor)
        {
            _baseUrl = String.Concat(configuration["OrderUrl"], "/api/orders");
            _client = client;
            _contextAccessor = contextAccessor;
        }

        public async Task<string> GetToken()
        {
            return await _contextAccessor.HttpContext.GetTokenAsync("access_token");
        }
        public async Task<int> CreateOrder(Order order)
        {
            var token = await GetToken();
            string path = APIPaths.Order.CreateOrder(_baseUrl);
            var response = await _client.PostAsync(path, order, token);
            var json = response.Content.ReadAsStringAsync();
            json.Wait();
            dynamic data = JObject.Parse(json.Result);
            return Convert.ToInt32(data.orderId);
        }

        public async Task<Order> GetOrder(string orderId)
        {
            var token = await GetToken();
            string path = APIPaths.Order.GetOrder(_baseUrl, orderId);
            var response = await _client.GetStringAsync(path, token);
            var data = JsonConvert.DeserializeObject<Order>(response);
            return data;
        }
    }
}

