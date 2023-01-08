using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;
using WebMvc.Infrastructure;
using WebMvc.Models;

namespace WebMvc.Services
{
    public class EventCatalogService : IEventCatalogService
    {
        private readonly IHttpClient _httpClient;
        private readonly string _baseUri;

        public EventCatalogService (IConfiguration config, IHttpClient httpClient) {
                _httpClient = httpClient;
                _baseUri = $"{config["EventCatalogUrl"]}/api/eventcatalog";
        }
            
        public async Task<EventCatalog> GetEventCatalogItemsAsync(int page, int size, int? location, int? category, int? organizer)
        {
            var eventItemsUri = APIPaths.EventCatalog.GetAllEventItems(_baseUri, page, size, location, category, organizer);
            var data = await _httpClient.GetStringAsync(eventItemsUri);
            return JsonConvert.DeserializeObject<EventCatalog>(data); 
        }

        public async Task<IEnumerable<SelectListItem>> GetEventCategoriesAsync()
        {
            var eventCategoriesUri = APIPaths.EventCatalog.GetAllEventCategories(_baseUri);
            var data = await _httpClient.GetStringAsync(eventCategoriesUri);
            var items = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value = null,
                    Selected = true,
                    Text = "All"
                }
            };
            var eventCategories = JArray.Parse(data);
            foreach (var item in eventCategories)
            {
                items.Add(new SelectListItem
                {
                    Value = item.Value<string>("id"),
                    Text = item.Value<string>("category")
                });
            }
            return items;
        }

        public async Task<IEnumerable<SelectListItem>> GetEventLocationsAsync()
        {
            var eventLocationsUri = APIPaths.EventCatalog.GetAllEventLocations(_baseUri);
            var data = await _httpClient.GetStringAsync(eventLocationsUri);
            var items = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value = null,
                    Selected = true,
                    Text = "All"
                }
            };
            var eventLocations = JArray.Parse(data);
            foreach (var item in eventLocations)
            {
                items.Add(new SelectListItem
                {
                    Value = item.Value<string>("id"),
                    Text = item.Value<string>("locationName")
                });
            }
            return items;
        }

        public async Task<IEnumerable<SelectListItem>> GetEventOrganizersAsync()
        {
            var eventOrganizersUri = APIPaths.EventCatalog.GetAllEventOrganizers(_baseUri);
            var data = await _httpClient.GetStringAsync(eventOrganizersUri);
            var items = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value = null,
                    Selected = true,
                    Text = "All"
                }
            };
            var eventOrganizers = JArray.Parse(data);
            foreach (var item in eventOrganizers)
            {
                items.Add(new SelectListItem
                {
                    Value = item.Value<string>("id"),
                    Text = item.Value<string>("organizerName")
                });
            }
            return items;
        }

    }
}
