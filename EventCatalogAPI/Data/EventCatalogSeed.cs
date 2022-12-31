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

            if (!context.EventOrganizers.Any())
            {
                context.EventOrganizers.AddRange(GetEventOrganizers());
                context.SaveChanges();
            }
            if (!context.EventItems.Any())
            {
                context.EventItems.AddRange(GetEventCatalogItems());
                context.SaveChanges();
            }    

        }

        private static IEnumerable<EventItem> GetEventCatalogItems()
        {
            return new List<EventItem>()
            {                
                new EventItem { EventLocationId = 2, EventCategoryId = 1, EventOrganizerId = 1, Description = "Our company is saying farewell to our beloved coworker!", Name = "Happy Retirement", EventAddress = "Hyatt Regency", Date =Convert.ToDateTime("04/21/2023"), StartTime=Convert.ToDateTime("6:30PM"), EndTime=Convert.ToDateTime("8:30PM"), Price = 20.5M, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/1" },
                new EventItem { EventLocationId = 1, EventCategoryId = 1, EventOrganizerId = 2, Description = "Welcoming the newest addition to the workplace!", Name = "Coworker's Baby Shower", EventAddress = "Kell's Irish Restaurant", Date =Convert.ToDateTime("05/01/2023"), StartTime=Convert.ToDateTime("12:30PM"), EndTime=Convert.ToDateTime("2:30PM"), Price = 15.5M, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/2" },
                new EventItem { EventLocationId = 2, EventCategoryId = 1, EventOrganizerId = 3, Description = "Making memories with the company", Name = "Company Team Building", EventAddress = "Marriot Grand", Date =Convert.ToDateTime("07/22/2023"), StartTime=Convert.ToDateTime("7:30AM"), EndTime=Convert.ToDateTime("10:30AM"), Price = 30M, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/3" },
                new EventItem { EventLocationId = 2, EventCategoryId = 1, EventOrganizerId = 2, Description = "Wind down from a long work week", Name = "Workplace Retreat", EventAddress = "Trinity Club", Date =Convert.ToDateTime("02/05/2023"), StartTime=Convert.ToDateTime("10:00AM"), EndTime=Convert.ToDateTime("9:00PM"), Price = 25M, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/4" },
                new EventItem { EventLocationId = 2, EventCategoryId = 1, EventOrganizerId = 1, Description = "Join us for our annual convention", Name = "Keyboard Convention", EventAddress = "Microsoft Convention Center", Date =Convert.ToDateTime("11/14/2023"), StartTime=Convert.ToDateTime("12:30PM"), EndTime=Convert.ToDateTime("5:30PM"), Price = 20.5M, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/5" },
                new EventItem { EventLocationId = 2, EventCategoryId = 2, EventOrganizerId = 1, Description = "Sleep under the stars", Name = "Community Camping Trip",  EventAddress = "Yosemite Park", Date =Convert.ToDateTime("08/30/2023"), StartTime=Convert.ToDateTime("07:00AM"), EndTime=Convert.ToDateTime("11:30PM"),Price = 15M, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/6" },
                new EventItem { EventLocationId = 2, EventCategoryId = 2, EventOrganizerId = 1, Description = "Fun for the whole family!", Name = "Cookie Decorating Party",  EventAddress = "New york Bakery", Date =Convert.ToDateTime("12/01/2023"), StartTime=Convert.ToDateTime("10:00AM"), EndTime=Convert.ToDateTime("3:00PM"), Price = 30M, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/7" },
                new EventItem { EventLocationId = 1, EventCategoryId = 2, EventOrganizerId = 3, Description = "Be here for our grand opening", Name = "Amusement Park Opening", EventAddress = "Disney", Date =Convert.ToDateTime("07/16/2023"), StartTime=Convert.ToDateTime("11:30AM"), EndTime=Convert.ToDateTime("9:30PM"), Price = 55M, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/8" },
                new EventItem { EventLocationId = 1, EventCategoryId = 2, EventOrganizerId = 1, Description = "Welcoming all members of the family", Name = "Family Reunion", EventAddress = "Trinity Church", Date =Convert.ToDateTime("03/01/2023"), StartTime=Convert.ToDateTime("1:30PM"), EndTime=Convert.ToDateTime("7:30PM"), Price = 20M, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/9" },
                new EventItem { EventLocationId = 2, EventCategoryId = 2, EventOrganizerId = 2, Description = "The IceHouse is open for season!", Name = "Ice Skating Event", EventAddress = "Trinity Club", Date =Convert.ToDateTime("12/11/2023"), StartTime=Convert.ToDateTime("10:45AM"), EndTime=Convert.ToDateTime("5:45PM"), Price = 15M, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/10" },
                new EventItem { EventLocationId = 1, EventCategoryId = 3, EventOrganizerId = 1, Description = "Celebrating the first day of summer", Name = "Summer Pool Party",  EventAddress = "Red Star Club", Date =Convert.ToDateTime("06/08/2023"), StartTime=Convert.ToDateTime("10:00PM"), EndTime=Convert.ToDateTime("6:30PM"), Price = 20.5M, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/11" },
                new EventItem { EventLocationId = 2, EventCategoryId = 3, EventOrganizerId = 3, Description = "Bring blankets and popcorn", Name = "Outdoor Movie Night", EventAddress = "Marymoor Park", Date =Convert.ToDateTime("07/16/2023"), StartTime=Convert.ToDateTime("7:30PM"), EndTime=Convert.ToDateTime("9:30PM"), Price = 15.5M, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/12" },
                new EventItem { EventLocationId = 3, EventCategoryId = 3, EventOrganizerId = 2, Description = "Enjoy a relaxing night", Name = "Paint and Sip",  EventAddress = "Painting Arena", Date =Convert.ToDateTime("01/16/2023"), StartTime=Convert.ToDateTime("8:00PM"), EndTime=Convert.ToDateTime("10:00PM"), Price = 35M, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/13" },
                new EventItem { EventLocationId = 3, EventCategoryId = 3, EventOrganizerId = 2, Description = "Show off your knowledge skills", Name = "Trivia Night",  EventAddress = "Statesman Arena", Date =Convert.ToDateTime("09/02/2023"), StartTime=Convert.ToDateTime("8:30PM"), EndTime=Convert.ToDateTime("10:30PM"), Price = 10.5M, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/14" },
                new EventItem { EventLocationId = 3, EventCategoryId = 3, EventOrganizerId = 3, Description = "Ring in the new year!", Name = "New Years Eve Countdown", EventAddress = "Country Club", Date =Convert.ToDateTime("12/31/2023"), StartTime=Convert.ToDateTime("9:00PM"), EndTime=Convert.ToDateTime("12:00AM"),Price = 30M, PictureUrl = "http://externalcatalogbaseurltobereplaced/api/pic/15" }
            };
        }

        private static IEnumerable<EventCategory> GetEventCategories()
        {
            return new List<EventCategory>
            {
                new EventCategory {Category = "Work"},
                new EventCategory {Category = "Kids"},
                new EventCategory {Category = "Social"}
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

        private static IEnumerable<EventOrganizer> GetEventOrganizers()
        {
            return new List<EventOrganizer>
            {
                new EventOrganizer {OrganizerName = "Rick Hartman"},
                new EventOrganizer {OrganizerName = "David Tutera"},
                new EventOrganizer {OrganizerName = "Mindy Weiss"}
            };
        }


    }
}
