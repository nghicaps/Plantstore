using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Plantstore.Models
{
    public class Validate
    {
        private const string LightLevelKey = "validLightLevel";
        private const string ScientificNameKey = "validScientificName";

        private ITempDataDictionary tempData { get; set; }
        public Validate(ITempDataDictionary temp) => tempData = temp;

        public bool IsValid { get; private set; }
        public string ErrorMessage { get; private set; }

        public void CheckLightLevel(string lightLevelId, Repository<LightLevel> data)
        {
            LightLevel? entity = data.Get(lightLevelId);
            IsValid = (entity == null) ? true : false;
            ErrorMessage = (IsValid) ? "" :
                $"Light level ID {lightLevelId} is already in the database.";
        }
        public void MarkLightLevelChecked() => tempData[LightLevelKey] = true;
        public void ClearLightLevel() => tempData.Remove(LightLevelKey);
        public bool IsLightLevelChecked => tempData.Keys.Contains(LightLevelKey);

        public void CheckScientificName(string genus, string species, string operation, Repository<ScientificName> data)
        {
            ScientificName? entity = null;
            if (Operation.IsAdd(operation))
            {
                entity = data.Get(new QueryOptions<ScientificName>
                {
                    Where = a => a.Genus == genus && a.Species == species
                });
            }
            IsValid = (entity == null) ? true : false;
            ErrorMessage = (IsValid) ? "" :
                $"ScientificName {entity.FullName} is already in the database.";
        }
        public void MarkScientificNameChecked() => tempData[ScientificNameKey] = true;
        public void ClearScientificName() => tempData.Remove(ScientificNameKey);
        public bool IsScientificNameChecked => tempData.Keys.Contains(ScientificNameKey);
    }
}