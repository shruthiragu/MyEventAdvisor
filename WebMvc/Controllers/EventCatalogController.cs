using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMvc.Services;
using WebMvc.ViewModels;

namespace WebMvc.Controllers
{
    public class EventCatalogController : Controller
    {
        private readonly IEventCatalogService _eventCatalogService;
        public EventCatalogController(IEventCatalogService eventCatalogService)
        {
            _eventCatalogService = eventCatalogService;

        }

        public async Task<IActionResult> Index (int? page, int? locationFilterApplied, int? categoryFilterApplied, int? organizerFilterApplied)
        {
            var itemsPerPage = 10;
            var eventCatalog = await _eventCatalogService.GetEventCatalogItemsAsync(page ?? 0, itemsPerPage, 
                locationFilterApplied, categoryFilterApplied, organizerFilterApplied);
            var vm = new EventCatalogIndexViewModel
            {
                Location = await _eventCatalogService.GetEventLocationsAsync(),
                Category = await _eventCatalogService.GetEventCategoriesAsync(),
                Organizer = await _eventCatalogService.GetEventOrganizersAsync(),
                EventItems = eventCatalog.Data,
                LocationFilterApplied = locationFilterApplied,
                CategoryFilterApplied = categoryFilterApplied,
                OrganizerFilterApplied = organizerFilterApplied,
                PaginationInfo = new PaginationInfo
                {
                    TotalItems = eventCatalog.Count,
                    ItemsPerPage = eventCatalog.PageSize,
                    ActualPage = eventCatalog.PageIndex,
                    TotalPages = (int)Math.Ceiling((decimal)eventCatalog.Count / itemsPerPage)
                }
            };
            return View(vm);

        }

        [Authorize]
        public IActionResult About()
        {
            return View();
        }

    }
}
