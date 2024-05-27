using Microsoft.AspNetCore.Mvc;
using ST10158190Part1.Models;

namespace ST10158190Part1.Controllers
{
    public class ProductDisplayController : Controller
    {
        public IActionResult Index()
        {
            var products = ProductDisplayModel.SelectProducts();
            return View();
        }
    }
}
