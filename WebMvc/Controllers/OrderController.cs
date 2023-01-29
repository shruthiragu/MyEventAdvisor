using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using WebMvc.Models;
using WebMvc.Models.OrderModels;
using WebMvc.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebMvc.Controllers
{
    public class OrderController : Controller
    {
        private readonly ICartService _cartService;
        public readonly IOrderService _orderService;
        private readonly IIdentityService<ApplicationUser> _identityService;
        private readonly IConfiguration _configuration;
        public OrderController(ICartService cartService, IOrderService orderService,
            IIdentityService<ApplicationUser> identityService, IConfiguration configuration)
        {
            _cartService = cartService;
            _orderService = orderService;
            _identityService = identityService;
            _configuration = configuration;
        }
        public async Task<IActionResult> Create()
        {
            var user = _identityService.Get(HttpContext.User);
            var cart = await _cartService.GetCart(user);
            var order = _cartService.MapCartToOrder(cart);
            ViewBag.StripePublishableKey = _configuration["StripePublicKey"];
            return View(order);
        }
        public IActionResult CreateOrder(Order formOrder)
        {
            var user = _identityService.Get(HttpContext.User);
            var order = formOrder;
            order.BuyerId = user.Id;
            order.UserName = user.Id;
            order.OrderDate = DateTime.UtcNow;
            order.OrderStatus = OrderStatus.Preparing;
            var options = new RequestOptions
            {
                ApiKey = _configuration["StripePrivateKey"]
            };
        }
        }
    }
}

