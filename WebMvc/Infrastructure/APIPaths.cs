namespace WebMvc.Infrastructure
{
    public static class APIPaths
    {
        public static class EventCatalog
        {
            public static string GetAllEventCategories (string baseUrl)
            {
                return $"{baseUrl}/eventcategories";
            }

            public static string GetAllEventLocations (string baseUrl)
            {
                return $"{baseUrl}/eventlocations";
            }

            public static string GetAllEventOrganizers (string baseUrl)
            {
                return $"{baseUrl}/eventorganizers";
            }

            public static string GetAllEventItems (string baseUrl, int page, int take)
            {
                return $"{baseUrl}/eventitems?pageIndex={page}&pageSize={take}";
            }



        }


    }
}
