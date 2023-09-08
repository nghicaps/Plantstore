using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Plantstore.Models
{
    public class LightLevel
    {
        [MaxLength(10)]
        [Required(ErrorMessage = "Please enter a light level id.")]
        [Remote("CheckLightLevel", "Validation", "Admin")]
        public string LightLevelId { get; set; }

        [StringLength(25)]
        [Required(ErrorMessage = "Please enter a light level name.")]
        public string Name { get; set; }

        public ICollection<Plant> Plants { get; set; }
    }
}