using Microsoft.AspNetCore.Mvc;
using ST10158190Part1.Models;

namespace ST10158190Part1.Controllers
{
    public class UsersController : Controller
    {
        public User usrtbl = new User();

        [HttpPost]
        public ActionResult Signup(User users)
        {
            var result = usrtbl.insert_User(users);
            if (result > 0)
            {
                return RedirectToAction("MyWork", "Home");
            }
            else
            {
                // Handle the case where the insert operation failed
                ModelState.AddModelError("", "An error occurred while signing up. Please try again.");
                return View(users);
            }
        }

        [HttpGet]
        public ActionResult Signup()
        {
            return View(new User());
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
