using Microsoft.AspNetCore.Mvc;
using ST10158190Part1.Models;

namespace ST10158190Part1.Controllers
{
    public class ProductsController : Controller
    {
        [HttpGet]
        public IActionResult MyWork()
        {
            return View(new Product());
        }

        [HttpPost]
        public IActionResult MyWork(Product product)
        {
            if (ModelState.IsValid)
            {
                var result = Product.InsertProduct(product);
                if (result > 0)
                {
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    ModelState.AddModelError("", "An error occurred while inserting the product.");
                }
            }

            return View(product);
        }
        [HttpPost]
        public IActionResult PlaceOrder(int userId, int productId, int quantity)
        {
            Product product = Product.GetAllProducts().Find(p => p.ProductId == productId);
            if (product != null)
            {
                decimal totalPrice = product.ProductPrice * quantity;
               // Transaction.PlaceOrder(userId, productId, quantity, totalPrice);
            }
            return RedirectToAction("MyWork");
        }
    }
}
