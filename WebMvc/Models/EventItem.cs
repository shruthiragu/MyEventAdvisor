namespace WebMvc.Models
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
        public string EventAddress { get; set; }


        public int EventLocationId { get; set; }
        public int EventCategoryId { get; set; }
        public int EventOrganizerId { get; set; }

        public EventLocation EventLocation{ get; set; }
        public EventCategory EventCategory { get; set; }
        public EventOrganizer EventOrganizer { get; set; }
    }
}
