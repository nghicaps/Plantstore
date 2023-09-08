using Microsoft.AspNetCore.Mvc;
using Plantstore.Models;

namespace Plantstore.Controllers
{
    public class HomeController : Controller
    {
        private Repository<Plant> data { get; set; }
        public HomeController(PlantstoreContext ctx) => data = new Repository<Plant>(ctx);

        public ViewResult Index()
        {
            var random = data.Get(new QueryOptions<Plant>
            {
                OrderBy = b => Guid.NewGuid()
            });

            return View(random);
        }

        public ContentResult Register()
        {
            return Content("Registration has not been implemented yet. It is implemented in chapter 16.");
        }

    }
}