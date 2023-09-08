using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Plantstore.Models
{
    public class ScientificName
    {
        public int ScientificNameId { get; set; }

        [Required(ErrorMessage = "Please enter a genus.")]
        [MaxLength(200)]
        public string? Genus { get; set; }

        [Required(ErrorMessage = "Please enter a species.")]
        [MaxLength(200)]
        [Remote("CheckScientificName", "Validation", "Admin", AdditionalFields = "Genus, Operation")]
        public string? Species { get; set; }

        public string FullName => $"{Genus} {Species}";

        public ICollection<Scientific>? Scientifics { get; set; }
    }
}

