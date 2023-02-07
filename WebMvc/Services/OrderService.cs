using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebMvc.Infrastructure;
using WebMvc.Models.OrderModels;

namespace WebMvc.Services
{
    public class OrderService : IOrderService
    {
        private readonly IConfiguration _config;
        private readonly IHttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger _logger;
        private readonly string _baseUri;
        public OrderService(IConfiguration config, IHttpContextAccessor httpContextAccessor, IHttpClient httpClient, ILoggerFactory logger)
        {
            _config = config;
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger.CreateLogger<OrderService>();
            _baseUri = $"{config["OrderUrl"]}/api/orders";
        }

        private async Task<string> GetUserTokenAsync()
        {
            var context = _httpContextAccessor.HttpContext;
            return await context.GetTokenAsync("access_token");
        }

        public async Task<int> CreateOrder(Order order)
        {
            var token = await GetUserTokenAsync();
            var createOrderUri = APIPaths.Order.AddNewOrder(_baseUri);
            _logger.LogDebug("Order Uri: " + createOrderUri);

            var response = await _httpClient.PostAsync(createOrderUri, order, token);

            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                throw new Exception("Error creating order. Try later...");
            }

            var jsonString = response.Content.ReadAsStringAsync();
            jsonString.Wait();
            dynamic data = JObject.Parse(jsonString.Result);
            string value = data.orderId;
            return Convert.ToInt32(value);

        }

        public async Task<Order> GetOrder(int orderId)
        {
            var token = await GetUserTokenAsync();
            var getOrderUri = APIPaths.Order.GetOrder(_baseUri, orderId);
            var dataString = await _httpClient.GetStringAsync(getOrderUri, token);
            var response = JsonConvert.DeserializeObject<Order>(dataString);
            return response;
        }
    }
}