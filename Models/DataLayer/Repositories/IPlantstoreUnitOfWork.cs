namespace Plantstore.Models
{
    public interface IPlantstoreUnitOfWork
    {
        Repository<Plant> Plants { get; }
        Repository<ScientificName> ScientificNames { get; }
        Repository<Scientific> Scientifics { get; }
        Repository<LightLevel> LightLevels { get; }

        void DeleteCurrentScientifics(Plant plant);
        void AddNewScientifics(Plant plant, int[] scientificNameids);
        void Save();
    }
}
