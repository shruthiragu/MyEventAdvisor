using EventCatalogAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventCatalogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventCatalogController : ControllerBase
    {
        private readonly EventCatalogContext _context;
        private readonly IConfiguration _configuration;

        public EventCatalogController (EventCatalogContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> EventLocations()
        {
            var locations = await _context.EventLocations.ToListAsync();
            return Ok(locations);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> EventCategories()
        {
            var categories = await _context.EventCategories.ToListAsync();
            return Ok(categories);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Items(
                [FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 6)
        {
            var eventCount = _context.EventItems.LongCountAsync();
            var events = await _context.EventItems.OrderByDescending(c => c.Date)
                                                .Skip(pageIndex * pageSize)
                                                .Take(pageSize)
                                                .ToListAsync();
            return Ok(events);
        }
    }
}
