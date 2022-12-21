using EventCatalogAPI.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace EventCatalogAPI.Data
{
    public class EventCatalogSeed
    {
        public static void Seed (EventCatalogContext context)
        {
            context.Database.Migrate();
            if (!context.EventItems.Any())
            {
                context.EventItems.AddRange(GetEventCatalogItems());
                context.SaveChanges();
            }
            if (!context.EventLocations.Any())
            {
                context.EventLocations.AddRange(GetEventLocations());
                context.SaveChanges();
            }
            if (!context.EventCategories.Any())
            {
                context.EventCategories.AddRange(GetEventCategories());
                context.SaveChanges();
            }

        }

        private static IEnumerable<EventItem> GetEventCatalogItems()
        {
            return new List<EventItem>()
            {
                new EventItem { EventLocationId = 2, EventCategoryId = 3, Description = "A ring that has been around for over 100 years", Name = "World Star", Price = 199.5M, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/1" },
                new EventItem { EventLocationId = 1, EventCategoryId = 2, Description = "will make you world champions", Name = "White Line", Price = 88.50M, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/2" },
                new EventItem { EventLocationId = 2, EventCategoryId = 3, Description = "You have already won gold medal", Name = "Prism White", Price = 129, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/3" },
                new EventItem { EventLocationId = 2, EventCategoryId = 2, Description = "Olympic runner", Name = "Foundation Hitech", Price = 12, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/4" },
                new EventItem { EventLocationId = 2, EventCategoryId = 1, Description = "Roslyn Red Sheet", Name = "Roslyn White", Price = 188.5M, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/5" },
                new EventItem { EventLocationId = 2, EventCategoryId = 2, Description = "Lala Land", Name = "Blue Star", Price = 112, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/6" },
                new EventItem { EventLocationId = 2, EventCategoryId = 1, Description = "High in the sky", Name = "Roslyn Green", Price = 212, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/7" },
                new EventItem { EventLocationId = 1, EventCategoryId = 1, Description = "Light as carbon", Name = "Deep Purple", Price = 238.5M, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/8" },
                new EventItem { EventLocationId = 1, EventCategoryId = 2, Description = "High Jumper", Name = "Antique Ring", Price = 129, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/9" },
                new EventItem { EventLocationId = 2, EventCategoryId = 3, Description = "Dunker", Name = "Elequent", Price = 12, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/10" },
                new EventItem { EventLocationId = 1, EventCategoryId = 2, Description = "All round", Name = "Inredeible", Price = 248.5M, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/11" },
                new EventItem { EventLocationId = 2, EventCategoryId = 1, Description = "Pricesless", Name = "London Sky", Price = 412, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/12" },
                new EventItem { EventLocationId = 3, EventCategoryId = 3, Description = "You ar ethe star", Name = "Elequent", Price = 123, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/13" },
                new EventItem { EventLocationId = 3, EventCategoryId = 2, Description = "A ring popular in the 16th and 17th century in Western Europe that was used as an engagement wedding ring", Name = "London Star", Price = 218.5M, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/14" },
                new EventItem { EventLocationId = 3, EventCategoryId = 1, Description = "A floppy, bendable ring made out of links of metal", Name = "Paris Blues", Price = 312, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/15" }
            };
        }

        private static IEnumerable<EventCategory> GetEventCategories()
        {
            return new List<EventCategory>
            {
                new EventCategory {Category = "Kids"},
                new EventCategory {Category = "Spiritual"},
                new EventCategory {Category = "Educational"}
            };
        }

        private static IEnumerable<EventLocation> GetEventLocations()
        {
            return new List<EventLocation>
            {
                new EventLocation {LocationName = "Redmond"},
                new EventLocation {LocationName = "New York"},
                new EventLocation {LocationName = "Chicago"}
            };
        }


    }
}
