namespace Plantstore.Models
{
    public class ScientificNameListViewModel
    {
        public IEnumerable<ScientificName> ScientificNames { get; set; }
        public RouteDictionary CurrentRoute { get; set; }
        public int TotalPages { get; set; }
    }
}

