using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using WebMvc.Infrastructure;
using WebMvc.Models;
using WebMvc.Models.CartModels;

namespace WebMvc.Services
{
    public class CartService : ICartService
    {
        private readonly IConfiguration _config;
        private readonly IHttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CartService(IConfiguration config, IHttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _config = config;
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            _baseUrl = $"{config["CartUrl"]}/api/cart";
        }

        private async Task<string> GetUserTokenAsync()
        {
            var context = _httpContextAccessor.HttpContext;
            return await context.GetTokenAsync("access_token");
        }

        public async Task AddItemtoCart(ApplicationUser user, CartItem product)
        {
            var cart = await GetCart(user);
            var basketItem = cart.Items.Where(p => p.ProductId == product.ProductId).FirstOrDefault();
            if (basketItem == null)
            {
                cart.Items.Add(product);
            } else
            {
                basketItem.Quantity++;
            }
            await UpdateCart(cart);
        }

        public async Task ClearCart(ApplicationUser user)
        {
            var token = await GetUserTokenAsync();
            var clearCartUri = APIPaths.Basket.CleanBasket(_baseUrl, user.Email);
            await _httpClient.DeleteAsync(clearCartUri, token);
        }

        public async Task<Cart> GetCart(ApplicationUser user)
        {
            var token = await GetUserTokenAsync();
            var getBasketUri = APIPaths.Basket.GetBasket(_baseUrl, user.Email);
            var dataString = await _httpClient.GetStringAsync(getBasketUri, token);
            var response = JsonConvert.DeserializeObject<Cart>(dataString) ??
                new Cart
                {
                    BuyerId = user.Email
                };
            return response;
        }

        public async Task<Cart> SetQuantities(ApplicationUser user, Dictionary<string, int> quantities)
        {
            var basket = await GetCart(user);
            basket.Items.ForEach(x =>
            {
                if (quantities.TryGetValue(x.Id, out var quant))
                {
                    x.Quantity = quant;
                }
            });
            return basket;

        }

        public async Task<Cart> UpdateCart(Cart cart)
        {
            var token = await GetUserTokenAsync();
            var updateBasketUri = APIPaths.Basket.UpdateBasket(_baseUrl);
            var response = await _httpClient.PostAsync(updateBasketUri, cart, token);
            response.EnsureSuccessStatusCode();
            return cart;

        }
    }
}
