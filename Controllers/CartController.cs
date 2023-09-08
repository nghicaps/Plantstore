using Microsoft.AspNetCore.Mvc;
using Plantstore.Models;

namespace Plantstore.Controllers
{
    public class CartController : Controller
    {
        private Repository<Plant> data { get; set; }
        public CartController(PlantstoreContext ctx) => data = new Repository<Plant>(ctx);


        private Cart GetCart()
        {
            var cart = new Cart(HttpContext);
            cart.Load(data);
            return cart;
        }

        public ViewResult Index()
        {
            Cart cart = GetCart();

            var builder = new PlantsGridBuilder(HttpContext.Session);

            var vm = new CartViewModel
            {
                List = cart.List,
                Subtotal = cart.Subtotal,
                PlantGridRoute = builder.CurrentRoute
            };
            return View(vm);
        }

        [HttpPost]
        public RedirectToActionResult Add(int id)
        {
            var plant = data.Get(new QueryOptions<Plant>
            {
                Includes = "Scientifics.ScientificName, LightLevel",
                Where = b => b.PlantId == id
            });
            if (plant == null)
            {
                TempData["message"] = "Unable to add plant to cart.";
            }
            else
            {
                var dto = new PlantDTO();
                dto.Load(plant);
                CartItem item = new CartItem
                {
                    Plant = dto,
                    Quantity = 1
                };

                Cart cart = GetCart();
                cart.Add(item);
                cart.Save();

                TempData["message"] = $"{plant.Title} added to cart";
            }

            var builder = new PlantsGridBuilder(HttpContext.Session);
            return RedirectToAction("List", "Plant", builder.CurrentRoute);
        }

        [HttpPost]
        public RedirectToActionResult Remove(int id)
        {
            Cart cart = GetCart();
            CartItem item = cart.GetById(id);
            cart.Remove(item);
            cart.Save();

            TempData["message"] = $"{item.Plant.Title} removed from cart.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public RedirectToActionResult Clear()
        {
            Cart cart = GetCart();
            cart.Clear();
            cart.Save();

            TempData["message"] = "Cart cleared.";
            return RedirectToAction("Index");
        }


        public IActionResult Edit(int id)
        {
            Cart cart = GetCart();
            CartItem item = cart.GetById(id);
            if (item == null)
            {
                TempData["message"] = "Unable to locate cart item";
                return RedirectToAction("List");
            }
            else
            {
                return View(item);
            }
        }

        [HttpPost]
        public RedirectToActionResult Edit(CartItem item)
        {
            Cart cart = GetCart();
            cart.Edit(item);
            cart.Save();

            TempData["message"] = $"{item.Plant.Title} updated";
            return RedirectToAction("Index");
        }

        public ViewResult Checkout() => View();
    }
}