using la_mia_pizzeria_post.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace la_mia_pizzeria_post.Controllers
{
    public class PizzaController : Controller
    {

        //public List<Pizza> getPizze()
        //{
        //    //List<Pizza> pizzeList = new()
        //    //{
        //    //    new Pizza("Margherita", "pomodoro, mozzarella", "/img/pizza1.png", 10.99F ),
        //    //    new Pizza("Marinara", "pomodoro, aglio rosmarino", "/img/pizza2.png", 7.99F),
        //    //    new Pizza("Lombarda", "pomodoro, mozzarella, carciofi, grana", "/img/pizza3.png", 11.99F),
        //    //    new Pizza("Pugliese", "pomodoro, mozzarella, capperi, aciughe", "/img/pizza4.png", 10.99F)

        //    //};
        //    //return pizzeList;


        //}

        public IActionResult Index()
        {
            // Read
           
                return View("Home");

        }
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

        public IActionResult Show(int id)
        {
            using (PizzeriaContext db = new())
            {
                Pizza searchedPizza = db.Pizzas.Where(pizza=>pizza.Id == id).FirstOrDefault();
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