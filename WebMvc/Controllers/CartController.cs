using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Polly.CircuitBreaker;
using WebMvc.Models;
using WebMvc.Models.CartModels;
using WebMvc.Services;

namespace WebMvc.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IIdentityService<ApplicationUser> _identityService;
        private readonly ICartService _cartService;
        private readonly IEventCatalogService _eventCatalogService;
        public CartController (IIdentityService<ApplicationUser> identityService,
            ICartService cartService, IEventCatalogService eventCatalogService)
        {
            _identityService = identityService;
            _cartService = cartService;
            _eventCatalogService = eventCatalogService;
        }
        
        //Page that is shown when user clicks the cart icon on top right corner
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Dictionary<string, int> quantities, string action)
        {
            
            //This method is called when you click either update cart button or proceed to checkout button on cart page
            if (action == "[ Checkout ]")
            {
                return RedirectToAction("Create", "Order");
            }
            try
            {
                var clearUserCart = false;
                if (action == "[ Delete Cart ]")
                {
                    clearUserCart = true;
                }
                var user = _identityService.Get(HttpContext.User);
                var basket = await _cartService.SetQuantities(user, quantities, clearUserCart);
                var vm = await _cartService.UpdateCart(basket);
            } catch (BrokenCircuitException)
            {
                HandleBrokenCircuitException();
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(EventItem productDetails)
        {
            try
            {
                if (productDetails.Id > 0)
                {
                    var user = _identityService.Get(HttpContext.User);
                    var product = new CartItem
                    {
                        Id = Guid.NewGuid().ToString(),
                        Quantity = 1,
                        ProductName = productDetails.Name,
                        PictureUrl = productDetails.PictureUrl,
                        UnitPrice = productDetails.Price,
                        ProductId = productDetails.Id.ToString()
                    };
                    await _cartService.AddItemtoCart(user, product);
                }
            }
            catch (BrokenCircuitException)
            {
                HandleBrokenCircuitException();
            }
            return RedirectToAction("Index", "EventCatalog");
        }

        private void HandleBrokenCircuitException()
        {
            TempData["BasketInoperativeMessage"] = "Cart service is inoperative. Please try later.(Business Msg Due to Circuit-Breaker)";
        }
    }
}
