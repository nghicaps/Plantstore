using Microsoft.AspNetCore.Mvc;
using Plantstore.Models;

namespace Plantstore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ValidationController : Controller
    {
        private Repository<ScientificName> scientificNameData { get; set; }
        private Repository<LightLevel> lightLevelData { get; set; }

        public ValidationController(PlantstoreContext ctx)
        {
            scientificNameData = new Repository<ScientificName>(ctx);
            lightLevelData = new Repository<LightLevel>(ctx);
        }

        public JsonResult CheckLightLevel(string lightLevelId)
        {
            var validate = new Validate(TempData);
            validate.CheckLightLevel(lightLevelId, lightLevelData);
            if (validate.IsValid)
            {
                validate.MarkLightLevelChecked();
                return Json(true);
            }
            else
            {
                return Json(validate.ErrorMessage);
            }
        }

        public JsonResult CheckScientificName(string genus, string species, string operation)
        {
            var validate = new Validate(TempData);
            validate.CheckScientificName(genus, species, operation, scientificNameData);
            if (validate.IsValid)
            {
                validate.MarkScientificNameChecked();
                return Json(true);
            }
            else
            {
                return Json(validate.ErrorMessage);
            }
        }

    }
}