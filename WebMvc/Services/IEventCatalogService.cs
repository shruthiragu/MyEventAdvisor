using Microsoft.AspNetCore.Mvc.Rendering;
using WebMvc.Models;

namespace WebMvc.Services
{
    public interface IEventCatalogService
    {
        Task<EventCatalog> GetEventCatalogItemsAsync(int page, int take, int? location, int? category, int? organizer);
        Task<IEnumerable<SelectListItem>> GetEventLocationsAsync();
        Task<IEnumerable<SelectListItem>> GetEventCategoriesAsync();
        Task<IEnumerable<SelectListItem>> GetEventOrganizersAsync();
    }
}
