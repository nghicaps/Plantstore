namespace Plantstore.Models
{
    public class PlantQueryOptions : QueryOptions<Plant>
    {
        public void SortFilter(PlantsGridBuilder builder)
        {
            if (builder.IsFilterByLightLevel)
            {
                Where = b => b.LightLevelId == builder.CurrentRoute.LightLevelFilter;
            }
            if (builder.IsFilterByPrice)
            {
                if (builder.CurrentRoute.PriceFilter == "under7")
                    Where = b => b.Price < 7;
                else if (builder.CurrentRoute.PriceFilter == "7to14")
                    Where = b => b.Price >= 7 && b.Price <= 14;
                else
                    Where = b => b.Price > 14;
            }
            if (builder.IsFilterByScientificName)
            {
                int id = builder.CurrentRoute.ScientificNameFilter.ToInt();
                if (id > 0)
                    Where = b => b.Scientifics.Any(ba => ba.ScientificName.ScientificNameId == id);
            }

            if (builder.IsSortByLightLevel)
            {
                OrderBy = b => b.LightLevel.Name;
            }
            else if (builder.IsSortByPrice)
            {
                OrderBy = b => b.Price;
            }
            else
            {
                OrderBy = b => b.Title;
            }
        }
    }
}
