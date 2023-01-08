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
                    //filterQs = (filterQs == string.Empty) ? $"categoryId={category.Value}" :
                      //  $"{filterQs}&categoryId={category.Value}";
                    filterQ = (filterQ == string.Empty) ? $"eventCategoryId={category.Value}" : $"{filterQ}&eventCategoryId={category.Value}";

                    //filterQ = $"{filterQ}&eventCategoryId={category.Value}";
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
                    preUri = $"{baseUri}/eventitems?pageIndex={page}&pageSize={take}&{filterQ}";
                    //preUri = $"{baseUri}/eventitems?pageIndex={page}&pageSize={take}{filterQ}";
                }

                return preUri;                
            }



        }


    }
}
