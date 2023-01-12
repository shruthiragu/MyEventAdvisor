using EventCatalogAPI.Data;
using EventCatalogAPI.Domain;
using EventCatalogAPI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

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
        public async Task<IActionResult> EventOrganizers()
        {
            var organizers = await _context.EventOrganizers.ToListAsync();
            return Ok(organizers);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> EventItems(
                [FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 6)
        {
            var eventCount = _context.EventItems.LongCountAsync();
            var events = await _context.EventItems.OrderByDescending(c => c.Date)
                                                .Skip(pageIndex * pageSize)
                                                .Take(pageSize)
                                                .Include(e => e.EventLocation)
                                                .Include(e => e.EventCategory)
                                                .Include(e => e.EventOrganizer)                                                
                                                .ToListAsync();
            
            events = ChangePictureUrl(events);
            var model = new PaginatedItemsViewModel
            {
                PageIndex = pageIndex,
                PageSize = events.Count,
                Data = events,
                Count = eventCount.Result
            };
            return Ok(model);
        }

        [HttpGet("[action]/filter")]
        public async Task<IActionResult> EventItems(
            [FromQuery] int? eventLocationId, [FromQuery] int? eventCategoryId, [FromQuery] int? eventOrganizerId,
               [FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 6)
        {
            var query = (IQueryable<EventItem>)_context.EventItems;
            if (eventLocationId.HasValue)
            {
                query = query.Where(l => l.EventLocationId == eventLocationId.Value);
            }
            if(eventCategoryId.HasValue)
            {
                query = query.Where(l => l.EventCategoryId == eventCategoryId.Value);
            }
            if (eventOrganizerId.HasValue)
            {
                query = query.Where(l => l.EventOrganizerId == eventOrganizerId.Value);
            }

            var eventCount = await query.LongCountAsync();
            var events = await query.OrderByDescending(c => c.Date)
                                                .Skip(pageIndex * pageSize)
                                                .Take(pageSize)
                                                .Include(e => e.EventLocation)
                                                .Include(e => e.EventCategory)
                                                .Include(e => e.EventOrganizer)
                                                .ToListAsync();
            events = ChangePictureUrl(events);
            var model = new PaginatedItemsViewModel
            {
                PageIndex = pageIndex,
                PageSize = events.Count,
                Data = events,
                Count = eventCount
            };
            return Ok(model);
        }

        private List<EventItem> ChangePictureUrl(List<EventItem> events)
        {
            events.ForEach(e => e.PictureUrl = e.PictureUrl.Replace("http://externalcatalogbaseurltobereplaced", _configuration["ExternalBaseUrl"]));
            return events;
        }
    }
}
