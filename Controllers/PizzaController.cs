using la_mia_pizzeria_post.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;

namespace la_mia_pizzeria_post.Controllers
{
    public class PizzaController : Controller
    {

        public IActionResult Index()
        {
            return View("Home");
        }

        [HttpGet]
        public IActionResult Pizze()
        {
            using (PizzeriaContext db = new())
            {
                // Read
                Console.WriteLine("Recupero lista di Studenti");
                List<Pizza> pizzas = db.Pizzas.OrderBy(pizza => pizza.Id).ToList<Pizza>();
                return View("Index", pizzas);
            }
        }

        [HttpGet]
        public IActionResult Show(int id)
        {
            using (PizzeriaContext db = new())
            {
                Pizza searchedPizza = db.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();
                if (searchedPizza == null)
                {
                    return NotFound("Non è stata trovata nessuna corrispondenza");
                }
                else
                {
                    return View(searchedPizza);
                }
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pizza pizzaData)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", pizzaData);
            }
            using (PizzeriaContext db = new())
            {
                Pizza newPizza = new();
                newPizza.Id = pizzaData.Id;
                newPizza.Name = pizzaData.Name;
                newPizza.Description = pizzaData.Description;
                newPizza.Image = pizzaData.Image;
                newPizza.Price = pizzaData.Price;
                db.Pizzas.Add(newPizza);
                db.SaveChanges();
                return RedirectToAction("Pizze");
            }
        }
        


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}