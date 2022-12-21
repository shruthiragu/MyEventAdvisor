namespace EventCatalogAPI.Domain
{
    public class EventItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PictureUrl { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public int EventLocationId { get; set; }
        public int EventCategoryId { get; set; }

        public virtual EventLocation EventLocation { get; set; }
        public virtual EventCategory EventCategory { get; set; }
    }
}
