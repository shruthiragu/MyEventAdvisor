using Microsoft.AspNetCore.Mvc.Rendering;
using WebMvc.Models;

namespace WebMvc.ViewModels
{
    public class EventCatalogIndexViewModel
    {
        public IEnumerable<SelectListItem> Location { get; set; }
        public IEnumerable<SelectListItem> Organizer { get; set; }
        public IEnumerable<SelectListItem> Category { get; set; }
        public IEnumerable<EventItem> EventItems { get; set; }
        public PaginationInfo PaginationInfo { get; set; }
        public int? LocationFilterApplied { get; set; }
        public int? CategoryFilterApplied { get; set; }
        public int? OrganizerFilterApplied { get; set; }
    }
}
