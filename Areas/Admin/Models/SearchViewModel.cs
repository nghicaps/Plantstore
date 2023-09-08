using System.ComponentModel.DataAnnotations;

namespace Plantstore.Models
{
    public class SearchViewModel
    {
        public IEnumerable<Plant>? Plants { get; set; }
        [Required(ErrorMessage = "Please enter a search term.")]
        public string? SearchTerm { get; set; }
        public string? Type { get; set; }
        public string? Header { get; set; }
    }
}
