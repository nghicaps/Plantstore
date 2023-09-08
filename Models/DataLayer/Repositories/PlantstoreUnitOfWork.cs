namespace Plantstore.Models
{
    public class PlantstoreUnitOfWork : IPlantstoreUnitOfWork
    {
        private PlantstoreContext context { get; set; }
        public PlantstoreUnitOfWork(PlantstoreContext ctx)
        {
            context = ctx;
        }

        private Repository<Plant> plantData;
        public Repository<Plant> Plants
        {
            get
            {
                if (plantData == null)
                    plantData = new Repository<Plant>(context);
                return plantData;
            }
        }

        private Repository<ScientificName> scientificNameData;
        public Repository<ScientificName> ScientificNames
        {
            get
            {
                if (scientificNameData == null)
                    scientificNameData = new Repository<ScientificName>(context);
                return scientificNameData;
            }
        }

        private Repository<Scientific> scientificData;
        public Repository<Scientific> Scientifics
        {
            get
            {
                if (scientificData == null)
                    scientificData = new Repository<Scientific>(context);
                return scientificData;
            }
        }

        private Repository<LightLevel> lightLevelData;
        public Repository<LightLevel> LightLevels
        {
            get
            {
                if (lightLevelData == null)
                    lightLevelData = new Repository<LightLevel>(context);
                return lightLevelData;
            }
        }

        public void DeleteCurrentScientifics(Plant plant)
        {
            var currentScientificNames = Scientifics.List(new QueryOptions<Scientific>
            {
                Where = ba => ba.PlantId == plant.PlantId
            });
            foreach (Scientific ba in currentScientificNames)
            {
                Scientifics.Delete(ba);
            }
        }

        public void AddNewScientifics(Plant plant, int[] scientificNameids)
        {
            foreach (int id in scientificNameids)
            {
                Scientific ba =
                    new Scientific { PlantId = plant.PlantId, ScientificNameId = id };
                Scientifics.Insert(ba);
            }
        }

        public void Save() => context.SaveChanges();
    }
}