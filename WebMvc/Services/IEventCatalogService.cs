using Microsoft.AspNetCore.Mvc.Rendering;
using WebMvc.Models;

namespace WebMvc.Services
{
    public interface IEventCatalogService
    {
        Task<EventCatalog> GetEventCatalogItemsAsync(int page, int take);
        Task<IEnumerable<SelectListItem>> Get
    }
}
