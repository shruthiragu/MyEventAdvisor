namespace WebMvc.Models
{
    public class EventCatalog
    {
        public long Count { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<EventItem> Data { get; set; }
    }
}
