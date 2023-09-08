namespace Plantstore.Models
{
    public class PlantsGridBuilder : GridBuilder
    {
        public PlantsGridBuilder(ISession sess) : base(sess) { }

        public PlantsGridBuilder(ISession sess, PlantsGridDTO values,
            string defaultSortField) : base(sess, values, defaultSortField)
        {
            bool isInitial = values.LightLevel.IndexOf(FilterPrefix.LightLevel) == -1;
            routes.ScientificNameFilter = (isInitial) ? FilterPrefix.ScientificName + values.ScientificName : values.ScientificName;
            routes.LightLevelFilter = (isInitial) ? FilterPrefix.LightLevel + values.LightLevel : values.LightLevel;
            routes.PriceFilter = (isInitial) ? FilterPrefix.Price + values.Price : values.Price;

            SaveRouteSegments();
        }

        public void LoadFilterSegments(string[] filter, ScientificName scientificName)
        {
            if (scientificName == null)
            {
                routes.ScientificNameFilter = FilterPrefix.ScientificName + filter[0];
            }
            else
            {
                routes.ScientificNameFilter = FilterPrefix.ScientificName + filter[0]
                    + "-" + scientificName.FullName.Slug();
            }
            routes.LightLevelFilter = FilterPrefix.LightLevel + filter[1];
            routes.PriceFilter = FilterPrefix.Price + filter[2];
        }
        public void ClearFilterSegments() => routes.ClearFilters();

        string def = PlantsGridDTO.DefaultFilter;
        public bool IsFilterByScientificName => routes.ScientificNameFilter != def;
        public bool IsFilterByLightLevel => routes.LightLevelFilter != def;
        public bool IsFilterByPrice => routes.PriceFilter != def;

        public bool IsSortByLightLevel =>
            routes.SortField.EqualsNoCase(nameof(LightLevel));
        public bool IsSortByPrice =>
            routes.SortField.EqualsNoCase(nameof(Plant.Price));
    }
}
