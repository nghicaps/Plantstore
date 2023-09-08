using Microsoft.AspNetCore.Mvc;
using Plantstore.Models;

namespace Plantstore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PlantController : Controller
    {
        private PlantstoreUnitOfWork data { get; set; }
        public PlantController(PlantstoreContext ctx) => data = new PlantstoreUnitOfWork(ctx);

        public ViewResult Index()
        {
            var search = new SearchData(TempData);
            search.Clear();

            return View();
        }

        [HttpPost]
        public RedirectToActionResult Search(SearchViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var search = new SearchData(TempData)
                {
                    SearchTerm = vm.SearchTerm,
                    Type = vm.Type
                };
                return RedirectToAction("Search");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ViewResult Search()
        {
            var search = new SearchData(TempData);

            if (search.HasSearchTerm)
            {
                var vm = new SearchViewModel
                {
                    SearchTerm = search.SearchTerm
                };

                var options = new QueryOptions<Plant>
                {
                    Includes = "LightLevel, Scientifics.ScientificName"
                };
                if (search.IsPlant)
                {
                    options.Where = b => b.Title.Contains(vm.SearchTerm);
                    vm.Header = $"Search results for common plant name '{vm.SearchTerm}'";
                }
                if (search.IsScientificName)
                {
                    int index = vm.SearchTerm.LastIndexOf(' ');
                    if (index == -1)
                    {
                        options.Where = b => b.Scientifics.Any(
                            ba => ba.ScientificName.Genus.Contains(vm.SearchTerm) ||
                            ba.ScientificName.Species.Contains(vm.SearchTerm));
                    }
                    else
                    {
                        string genus = vm.SearchTerm.Substring(0, index);
                        string species = vm.SearchTerm.Substring(index + 1);
                        options.Where = b => b.Scientifics.Any(
                            ba => ba.ScientificName.Genus.Contains(genus) &&
                            ba.ScientificName.Species.Contains(species));
                    }
                    vm.Header = $"Search results for scientific name '{vm.SearchTerm}'";
                }
                if (search.IsLightLevel)
                {
                    options.Where = b => b.LightLevelId.Contains(vm.SearchTerm);
                    vm.Header = $"Search results for light level ID '{vm.SearchTerm}'";
                }
                vm.Plants = data.Plants.List(options);
                return View("SearchResults", vm);
            }
            else
            {
                return View("Index");
            }
        }

        [HttpGet]
        public ViewResult Add(int id) => GetPlant(id, "Add");

        [HttpPost]
        public IActionResult Add(PlantViewModel vm)
        {
            if (ModelState.IsValid)
            {
                data.AddNewScientifics(vm.Plant, vm.SelectedScientificNames);
                data.Plants.Insert(vm.Plant);
                data.Save();

                TempData["message"] = $"{vm.Plant.Title} added to plants.";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["message"] = $"{vm.Plant.Title} not added to plants.";
                Load(vm, "Add");
                return View("Plant", vm);
            }
        }

        [HttpGet]
        public ViewResult Edit(int id) => GetPlant(id, "Edit");

        [HttpPost]
        public IActionResult Edit(PlantViewModel vm)
        {
            if (ModelState.IsValid)
            {
                data.DeleteCurrentScientifics(vm.Plant);
                data.AddNewScientifics(vm.Plant, vm.SelectedScientificNames);
                data.Plants.Update(vm.Plant);
                data.Save();

                TempData["message"] = $"{vm.Plant.Title} updated.";
                return RedirectToAction("Search");
            }
            else
            {
                Load(vm, "Edit");
                return View("Plant", vm);
            }
        }

        [HttpGet]
        public ViewResult Delete(int id) => GetPlant(id, "Delete");

        [HttpPost]
        public IActionResult Delete(PlantViewModel vm)
        {
            data.Save();
            TempData["message"] = $"{vm.Plant.Title} removed from plants.";
            return RedirectToAction("Search");
        }

        private ViewResult GetPlant(int id, string operation)
        {
            var plant = new PlantViewModel();
            Load(plant, operation, id);
            return View("Plant", plant);
        }
        private void Load(PlantViewModel vm, string op, int? id = null)
        {
            if (Operation.IsAdd(op))
            {
                vm.Plant = new Plant();
            }
            else
            {
                vm.Plant = data.Plants.Get(new QueryOptions<Plant>
                {
                    Includes = "Scientifics.ScientificName, LightLevel",
                    Where = b => b.PlantId == (id ?? vm.Plant.PlantId)
                });
            }

            vm.SelectedScientificNames = vm.Plant.Scientifics?.Select(
                ba => ba.ScientificName.ScientificNameId).ToArray();
            vm.ScientificNames = data.ScientificNames.List(new QueryOptions<ScientificName>
            {
                OrderBy = a => a.Genus
            });
            vm.LightLevels = data.LightLevels.List(new QueryOptions<LightLevel>
            {
                OrderBy = g => g.Name
            });
        }

    }
}