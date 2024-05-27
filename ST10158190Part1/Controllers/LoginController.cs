using ST10158190Part1.Models;
using Microsoft.AspNetCore.Mvc;

namespace CloudDevelopment.Controllers
{
    public class LoginController : Controller
    {

        private readonly Login login;

        public LoginController()
        {
            login = new Login();
        }

        [HttpPost]
        public ActionResult Login(string email, string name)
        {
            var loginModel = new Login();
            int userID = loginModel.SelectUser(email, name);
            if (userID != -1)
            {
                // Store userID in session
               


                // User found, proceed with login logic (e.g., set authentication cookie)
                // For demonstration, redirecting to a dummy page
                return RedirectToAction("Index", "Home", new {userID = userID});
            }
            else
            {
                // User not found, handle accordingly (e.g., show error message)
                return View("LoginFailed");
            }
        }
    }
}
