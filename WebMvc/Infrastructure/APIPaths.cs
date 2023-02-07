namespace WebMvc.Infrastructure
{
    public static class APIPaths
    {
        public static class EventCatalog
        {
            public static string GetAllEventCategories (string baseUri)
            {
                return $"{baseUri}/eventcategories";
            }

            public static string GetAllEventLocations (string baseUri)
            {
                return $"{baseUri}/eventlocations";
            }

            public static string GetAllEventOrganizers (string baseUri)
            {
                return $"{baseUri}/eventorganizers";
            }

            public static string GetAllEventItems (string baseUri, int page, int take, int? location, int? category, int? organizer)
            {
                var preUri = string.Empty;
                var filterQ = string.Empty;
                if (location.HasValue)
                {
                    filterQ = $"eventLocationId={location.Value}";
                }
                if (category.HasValue)
                {                   
                    filterQ = (filterQ == string.Empty) ? $"eventCategoryId={category.Value}" : $"{filterQ}&eventCategoryId={category.Value}";                    
                }
                if (organizer.HasValue)
                {
                    filterQ = (filterQ == string.Empty) ? $"&eventOrganizerId={organizer.Value}" : $"{filterQ}&eventOrganizerId={organizer.Value}";
                }
                if (string.IsNullOrEmpty(filterQ))
                {
                    preUri = $"{baseUri}/eventitems?pageIndex={page}&pageSize={take}";
                }
                else
                {
                    preUri = $"{baseUri}/eventitems/filter?pageIndex={page}&pageSize={take}&{filterQ}";                    
                }

                return preUri;                
            }



        }

        public static class Basket
        {
            public static string GetBasket(string baseUri, string basketId)
            {
                return $"{baseUri}/{basketId}";
            }

            public static string UpdateBasket(string baseUri)
            {
                return baseUri;
            }

            public static string CleanBasket(string baseUri, string basketId)
            {
                return $"{baseUri}/{basketId}";
            }
        }

        public static class Order
        {
            public static string GetOrder(string baseUri, int? orderId)
            {
                return $"{baseUri}/getorder/{orderId.Value}";
            }

            public static string AddNewOrder(string baseUri)
            {
                return $"{baseUri}/new";
            }
        }

    }
}
