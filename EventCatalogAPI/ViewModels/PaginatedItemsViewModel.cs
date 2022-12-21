using EventCatalogAPI.Domain;

namespace EventCatalogAPI.ViewModels
{
    public class PaginatedItemsViewModel
    {
        public long Count { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<EventItem> Data { get; set; }
    }
}
