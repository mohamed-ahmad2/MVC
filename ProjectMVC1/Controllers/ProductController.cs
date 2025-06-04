using Microsoft.AspNetCore.Mvc;
using ProjectMVC1.Models;

namespace ProjectMVC1.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductsBL productsBL = new ProductsBL();
        public IActionResult Index()
        {
            var products = productsBL.GetAll();
            return View(products);
        }

        public IActionResult Details(int id)
        {
            var product = productsBL.GetById(id);
            return View("Details", product);
        }
    }
}
