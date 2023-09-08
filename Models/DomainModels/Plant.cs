using Humanizer.Localisation;
using System.ComponentModel.DataAnnotations;

namespace Plantstore.Models
{
    public partial class Plant
    {
        public int PlantId { get; set; }

        [Required(ErrorMessage = "Please enter a title.")]
        [MaxLength(200)]
        public string Title { get; set; }

        [Range(0.0, 1000000.0, ErrorMessage = "Price must be more than 0.")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Please select a light level.")]
        public string LightLevelId { get; set; }

        public LightLevel LightLevel { get; set; }
        public ICollection<Scientific>? Scientifics { get; set; }
    }
}
