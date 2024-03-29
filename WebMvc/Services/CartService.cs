﻿using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using WebMvc.Infrastructure;
using WebMvc.Models;
using WebMvc.Models.CartModels;
using WebMvc.Models.OrderModels;

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

        public async Task<Cart> SetQuantities(ApplicationUser user, Dictionary<string, int> quantities, bool clearUserCart)
        {
            var basket = await GetCart(user);
            var itemsToRemove = new List<CartItem>();
            basket.Items.ForEach(x =>
            {
                if (clearUserCart)
                {
                    itemsToRemove.Add(x);
                }
                else
                {
                    if (quantities.TryGetValue(x.Id, out var quant))
                    {
                        if (quant > 0)
                            x.Quantity = quant;
                        else
                            itemsToRemove.Add(x);
                    }
                }
            });
            foreach(var item in itemsToRemove)
            {
                basket.Items.Remove(item);
            }
            
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

        public Order MapCartToOrder(Cart cart)
        {
            var order = new Order();
            order.OrderTotal = 0;

            cart.Items.ForEach(x =>
            {
                order.OrderItems.Add(new OrderItem()
                {                    
                    PictureUrl = x.PictureUrl,
                    ProductId = int.Parse(x.ProductId),
                    ProductName = x.ProductName,
                    UnitPrice = x.UnitPrice,
                    Units = x.Quantity
                });
                order.OrderTotal += x.UnitPrice * x.Quantity;
            });
            return order;
        }
    }
}
