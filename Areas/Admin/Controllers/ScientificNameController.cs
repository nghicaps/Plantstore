using Microsoft.AspNetCore.Mvc;
using Plantstore.Models;

namespace Plantstore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ScientificNameController : Controller
    {
        private Repository<ScientificName> data { get; set; }
        public ScientificNameController(PlantstoreContext ctx) => data = new Repository<ScientificName>(ctx);

        public ViewResult Index()
        {
            var scientificNames = data.List(new QueryOptions<ScientificName>
            {
                OrderBy = a => a.Genus
            });
            return View(scientificNames);
        }

        public RedirectToActionResult Select(int id, string operation)
        {
            switch (operation.ToLower())
            {
                case "view plants":
                    return RedirectToAction("ViewPlants", new { id });
                case "edit":
                    return RedirectToAction("Edit", new { id });
                case "delete":
                    return RedirectToAction("Delete", new { id });
                default:
                    return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ViewResult Add() => View("ScientificName", new ScientificName());

        [HttpPost]
        public IActionResult Add(ScientificName scientificName, string operation)
        {
            var validate = new Validate(TempData);
            if (!validate.IsScientificNameChecked)
            {
                validate.CheckScientificName(scientificName.Genus, scientificName.Species, operation, data);
                if (!validate.IsValid)
                {
                    ModelState.AddModelError(nameof(scientificName.Species), validate.ErrorMessage);
                }
            }

            if (ModelState.IsValid)
            {
                data.Insert(scientificName);
                data.Save();
                validate.ClearScientificName();
                TempData["message"] = $"{scientificName.FullName} added to scientific names.";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["message"] = $"{scientificName.FullName} not added to scientific names.";
                return View("ScientificName", scientificName);
            }
        }

        [HttpGet]
        public ViewResult Edit(int id) => View("ScientificName", data.Get(id));

        [HttpPost]
        public IActionResult Edit(ScientificName scientificName)
        {
            if (ModelState.IsValid)
            {
                data.Update(scientificName);
                data.Save();
                TempData["message"] = $"{scientificName.FullName} updated.";
                return RedirectToAction("Index");
            }
            else
            {
                return View("ScientificName", scientificName);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var scientificName = data.Get(new QueryOptions<ScientificName>
            {
                Includes = "Scientifics",
                Where = a => a.ScientificNameId == id
            });

            if (scientificName.Scientifics.Count > 0)
            {
                TempData["message"] = $"Can't delete scientific name {scientificName.FullName} because associated with these plants.";
                return GoToAuthorSearch(scientificName);
            }
            else
            {
                return View("ScientificName", scientificName);
            }
        }

        [HttpPost]
        public RedirectToActionResult Delete(ScientificName scientificName)
        {
            data.Delete(scientificName);
            data.Save();
            TempData["message"] = $"{scientificName.FullName} removed from scientific names.";
            return RedirectToAction("Index");
        }

        public RedirectToActionResult ViewPlants(int id)
        {
            var scientificName = data.Get(id);
            return GoToAuthorSearch(scientificName);
        }

        private RedirectToActionResult GoToAuthorSearch(ScientificName scientificName)
        {
            var search = new SearchData(TempData)
            {
                SearchTerm = scientificName.FullName,
                Type = "scientificName"
            };
            return RedirectToAction("Search", "Plant");
        }
    }
}