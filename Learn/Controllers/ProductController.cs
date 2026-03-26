using Learn.Models;
using Microsoft.AspNetCore.Mvc;

namespace Learn.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("/p/{id:int}")]
        public IActionResult Details(int id)
        {
            return View(new Product()
            {
                Id= id.ToString(),
                Name= $"Product Name: {id}(by int)"
            });
        }

        [Route("/p/{name}")]
        public IActionResult Details(string name)
        {
            return View(new Product()
            {
                Id = name,
                Name = $"Product Name: {name} (string)"
            });
        }
    }
}

