using Microsoft.AspNetCore.Mvc;
using Plantstore.Models;

namespace Plantstore.Controllers
{
    public class PlantController : Controller
    {
        private PlantstoreUnitOfWork data { get; set; }
        public PlantController(PlantstoreContext ctx) => data = new PlantstoreUnitOfWork(ctx);

        public RedirectToActionResult Index() => RedirectToAction("List");

        public ViewResult List(PlantsGridDTO values)
        {
            var builder = new PlantsGridBuilder(HttpContext.Session, values,
                defaultSortField: nameof(Plant.Title));

            var options = new PlantQueryOptions
            {
                Includes = "Scientifics.ScientificName, LightLevel",
                OrderByDirection = builder.CurrentRoute.SortDirection,
                PageNumber = builder.CurrentRoute.PageNumber,
                PageSize = builder.CurrentRoute.PageSize
            };
            options.SortFilter(builder);

            var vm = new PlantListViewModel
            {
                Plants = data.Plants.List(options),
                ScientificNames = data.ScientificNames.List(new QueryOptions<ScientificName>
                {
                    OrderBy = a => a.Genus
                }),
                LightLevels = data.LightLevels.List(new QueryOptions<LightLevel>
                {
                    OrderBy = g => g.Name
                }),
                CurrentRoute = builder.CurrentRoute,
                TotalPages = builder.GetTotalPages(data.Plants.Count)
            };

            return View(vm);
        }

        public ViewResult Details(int id)
        {
            var plant = data.Plants.Get(new QueryOptions<Plant>
            {
                Includes = "Scientifics.ScientificName, LightLevel",
                Where = b => b.PlantId == id
            });
            return View(plant);
        }

        [HttpPost]
        public RedirectToActionResult Filter(string[] filter, bool clear = false)
        {
            var builder = new PlantsGridBuilder(HttpContext.Session);

            if (clear)
            {
                builder.ClearFilterSegments();
            }
            else
            {
                var scientificName = data.ScientificNames.Get(filter[0].ToInt());
                builder.CurrentRoute.PageNumber = 1;
                builder.LoadFilterSegments(filter, scientificName);
            }

            builder.SaveRouteSegments();
            return RedirectToAction("List", builder.CurrentRoute);
        }

        [HttpPost]
        public RedirectToActionResult PageSize(int pagesize)
        {
            var builder = new PlantsGridBuilder(HttpContext.Session);

            builder.CurrentRoute.PageSize = pagesize;
            builder.SaveRouteSegments();

            return RedirectToAction("List", builder.CurrentRoute);
        }
    }
}