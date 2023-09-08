using System.ComponentModel.DataAnnotations;

namespace Plantstore.Models
{
    public class PlantViewModel : IValidatableObject
    {
        public Plant Plant { get; set; }
        public IEnumerable<LightLevel> LightLevels { get; set; }
        public IEnumerable<ScientificName> ScientificNames { get; set; }
        public int[] SelectedScientificNames { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext ctx)
        {
            if (SelectedScientificNames == null)
            {
                yield return new ValidationResult(
                  "Please select at least one scientific name.",
                  new[] { nameof(SelectedScientificNames) });
            }
        }

    }
}
