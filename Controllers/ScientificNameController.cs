using Microsoft.AspNetCore.Mvc;
using Plantstore.Models;

namespace Plantstore.Controllers
{
    public class ScientificNameController : Controller
    {
        private Repository<ScientificName> data { get; set; }
        public ScientificNameController(PlantstoreContext ctx) => data = new Repository<ScientificName>(ctx);

        public IActionResult Index() => RedirectToAction("List");

        public ViewResult List(GridDTO vals)
        {

            string defaultSort = nameof(ScientificName.Genus);
            var builder = new GridBuilder(HttpContext.Session, vals, defaultSort);
            builder.SaveRouteSegments();

            var options = new QueryOptions<ScientificName>
            {
                Includes = "Scientifics.Plant",
                PageNumber = builder.CurrentRoute.PageNumber,
                PageSize = builder.CurrentRoute.PageSize,
                OrderByDirection = builder.CurrentRoute.SortDirection
            };
            if (builder.CurrentRoute.SortField.EqualsNoCase(defaultSort))
                options.OrderBy = a => a.Genus;
            else
                options.OrderBy = a => a.Species;

            var vm = new ScientificNameListViewModel
            {
                ScientificNames = data.List(options),
                CurrentRoute = builder.CurrentRoute,
                TotalPages = builder.GetTotalPages(data.Count)
            };

            return View(vm);
        }

        public IActionResult Details(int id)
        {
            var scientificName = data.Get(new QueryOptions<ScientificName>
            {
                Includes = "Scientifics.Plant",
                Where = a => a.ScientificNameId == id
            });
            return View(scientificName);
        }
    }
}