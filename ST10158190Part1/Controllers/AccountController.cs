/* using Microsoft.AspNetCore.Mvc;
using ST10158190Part1.Models;
using System.Linq;
//using Microsoft.EntityFrameworkCore;
using ST10158190Part1.Models;

namespace ST10158190Part1.Controllers
{
    public class AccountController : Controller
    {
        private readonly YourDbContext _context;

        public AccountController(YourDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User model)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == model.Name && u.Password == model.Password);

            if (user != null)
            {
                // Authentication successful, redirect to a different page
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Authentication failed, show error message
                ModelState.AddModelError("", "Invalid username or password");
                return View();
            }
        }
    }
 */
