//using Common.Messaging;
//using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderAPI.Data;
using OrderAPI.Models;
using System.Net;

namespace OrderApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly OrdersContext _ordersContext;
        private readonly ILogger<OrdersController> _logger;
        private readonly IConfiguration _config;
        //private IPublishEndpoint _bus; //Configure order api to be a publisher; Inject it in constructor so that it can use it whenever necessary
        public OrdersController(OrdersContext ordersContext, ILogger<OrdersController> logger, IConfiguration config)
        {
            _ordersContext = ordersContext;
            _logger = logger;
            _config = config;
            //_bus = bus;
        }

        //api/order/getorder/{id}
        //[HttpGet("{id}")]
        //[HttpGet("{id}", Name = "[action]")]
        [HttpGet("[action]/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetOrder(int id)
        {
            var item = await _ordersContext.Orders
                                        .Include(x => x.OrderItems)
                                        .SingleOrDefaultAsync(ci => ci.OrderId == id);
            if (item != null)
            {
                return Ok(item);
            }
            return NotFound();

        }

        //api/order/new
        [Route("new")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            order.OrderStatus = OrderStatus.Preparing;
            order.OrderDate = DateTime.UtcNow;

            _logger.LogInformation("In create order");
            _logger.LogInformation("Order: " + order.UserName);

            _ordersContext.Orders.Add(order);
            _ordersContext.OrderItems.AddRange(order.OrderItems);

            _logger.LogInformation("Orders adding to context");
            _logger.LogInformation("Saving...");

            try
            {
                await _ordersContext.SaveChangesAsync();
                //_bus.Publish(new OrderCompletedEvent(order.BuyerId)).Wait();
                return Ok(new { order.OrderId });
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Order saving failed..." + ex.Message);
                return BadRequest();
            }

        }
    }
}