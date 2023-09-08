using Microsoft.AspNetCore.Mvc;
using Plantstore.Models;

namespace Bookstore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LightLevelController : Controller
    {
        private Repository<LightLevel> data { get; set; }
        public LightLevelController(PlantstoreContext ctx) => data = new Repository<LightLevel>(ctx);

        public ViewResult Index()
        {
            var search = new SearchData(TempData);
            search.Clear();

            var lightLevels = data.List(new QueryOptions<LightLevel>
            {
                OrderBy = g => g.Name
            });
            return View(lightLevels);
        }

        [HttpGet]
        public ViewResult Add() => View("LightLevel", new LightLevel());

        [HttpPost]
        public IActionResult Add(LightLevel lightLevel)
        {
            var validate = new Validate(TempData);
            if (!validate.IsLightLevelChecked)
            {
                validate.CheckLightLevel(lightLevel.LightLevelId, data);
                if (!validate.IsValid)
                {
                    ModelState.AddModelError(nameof(lightLevel.LightLevelId), validate.ErrorMessage);
                }
            }

            if (ModelState.IsValid)
            {
                data.Insert(lightLevel);
                data.Save();
                validate.ClearLightLevel();
                TempData["message"] = $"{lightLevel.Name} added to light levels.";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["message"] = $"{lightLevel.Name} not added to light levels.";
                return View("LightLevel", lightLevel);
            }
        }

        [HttpGet]
        public ViewResult Edit(string id) => View("LightLevel", data.Get(id));

        [HttpPost]
        public IActionResult Edit(LightLevel lightLevel)
        {
            if (ModelState.IsValid)
            {
                data.Update(lightLevel);
                data.Save();
                TempData["message"] = $"{lightLevel.Name} updated.";
                return RedirectToAction("Index");
            }
            else
            {
                return View("LightLevel", lightLevel);
            }
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            var lightLevel = data.Get(new QueryOptions<LightLevel>
            {
                Includes = "Plants",
                Where = g => g.LightLevelId == id
            });

            if (lightLevel.Plants.Count > 0)
            {
                TempData["message"] = $"Can't delete light Level {lightLevel.Name} "
                                    + "because associated with these plants.";
                return GoToPlantSearchResults(id);
            }
            else
            {
                return View("LightLevel", lightLevel);
            }
        }

        [HttpPost]
        public IActionResult Delete(LightLevel lightLevel)
        {
            data.Delete(lightLevel);
            data.Save();
            TempData["message"] = $"{lightLevel.Name} removed from light levels.";
            return RedirectToAction("Index");
        }

        public RedirectToActionResult ViewPlants(string id) => GoToPlantSearchResults(id);

        private RedirectToActionResult GoToPlantSearchResults(string id)
        {
            var search = new SearchData(TempData)
            {
                SearchTerm = id,
                Type = "lightLevel"
            };
            return RedirectToAction("Search", "Plant");
        }

    }
}