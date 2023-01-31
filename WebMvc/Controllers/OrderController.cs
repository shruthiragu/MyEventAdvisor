using Microsoft.AspNetCore.Mvc;
using Polly.CircuitBreaker;
using Stripe;
using WebMvc.Models;
using WebMvc.Models.OrderModels;
using WebMvc.Services;

namespace WebMvc.Controllers
{
    public class OrderController : Controller
    {
        private readonly IConfiguration _config;
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _orderService;
        private readonly IIdentityService<ApplicationUser> _identityService;
        private readonly ICartService _cartService;
        public OrderController(IConfiguration config, ILogger<OrderController> logger,
            IOrderService orderService, ICartService cartService, IIdentityService<ApplicationUser> identityService)
        {
            _cartService = cartService;
            _config = config;
            _logger = logger;
            _orderService = orderService;
            _identityService = identityService;
        }
        public async Task<IActionResult> Create()
        {
            var user = _identityService.Get(HttpContext.User);
            var cart = await _cartService.GetCart(user);
            var order = _cartService.MapCartToOrder(cart);
            ViewBag.StripePublishableKey = _config["StripePublicKey"];
            return View(order);
        }
        //frmOrder is the data sitting on the order page 
        [HttpPost]
        public async Task<IActionResult> Create(Order frmOrder)
        {
            //if (ModelState.IsValid)
            //{
                var user = _identityService.Get(HttpContext.User);
                var order = frmOrder;
                order.UserName = user.Email;
                order.BuyerId = user.Email;

                //Pass secret key before making a stripe call
                var options = new RequestOptions
                {
                    ApiKey = _config["StripePrivateKey"]
                };

                //getting ready to Make a chanrge to stripe
                var chargeOptions = new ChargeCreateOptions
                {
                    Amount = (int)order.OrderTotal * 100,
                    Currency = "usd",
                    Source = order.StripeToken,
                    Description = $"EventAdvisor Order payment : {order.UserName}",
                    ReceiptEmail = user.Email
                };
                var chargeService = new ChargeService();
                Charge stripeCharge = null;
                try
                {
                    stripeCharge = chargeService.Create(chargeOptions, options);
                }catch(StripeException ex)
                {
                    _logger.LogDebug("Stripe Exception  : " + ex.Message);
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(frmOrder);
                }

                //After payment, send data to microservice
                try
                {
                    if(stripeCharge.Id != null)
                    {
                        order.PaymentAuthCode = stripeCharge.Id;
                        int orderId = await _orderService.CreateOrder(order);
                        await _cartService.ClearCart(user);
                        return RedirectToAction("Complete", new {id = orderId, userName = user.UserName});
                    } else
                    {
                        ViewData["message"] = "Payment cannot be processed, try again.";
                        return View(frmOrder);
                    }
                } catch (BrokenCircuitException)
                {
                    ModelState.AddModelError("Error", "Sorry, not possible to create order. Try again.");
                    return View(frmOrder);
                }

            /*} else
            {
                return View(frmOrder);
            }*/
        }

        public IActionResult Complete (int id, string userName)
        {
            _logger.LogInformation($"User {userName} completed order");
            return View(id);
        }
    }
}
