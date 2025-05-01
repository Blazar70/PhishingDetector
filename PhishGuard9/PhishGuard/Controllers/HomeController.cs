using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhishGuard.Models;
using System;
using System.Diagnostics;

namespace PhishGuard.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Handle the login and set session + cookies
        [HttpPost]
        public IActionResult Login(UserLoginModel model)
        {
            if (ModelState.IsValid)
            {
                // Simulate user validation (Replace with real validation logic, e.g., checking the database)
                if (model.Email == "test@example.com" && model.Password == "password123") // Example check
                {
                    // Set session data (Store the user's name in session)
                    HttpContext.Session.SetString("UserName", model.Email);

                    // Set cookie
                    var cookieOptions = new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(7),  // Cookie expires in 7 days
                        HttpOnly = true,  // Ensures the cookie is not accessible via JavaScript
                        Secure = true     // Ensures cookie is sent over HTTPS only
                    };

                    Response.Cookies.Append("UserName", model.Email, cookieOptions);  // Set cookie

                    return RedirectToAction("Index", "Home");  // Redirect to Home page
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid email or password.");  // Error handling for failed login
                }
            }

            return View(model);  // Return to the login page if validation fails
        }

        // Display home page and retrieve session/cookie data
        public IActionResult Index()
        {
            // Retrieve session data
            var userName = HttpContext.Session.GetString("UserName");

            // Retrieve cookie data
            var userCookie = Request.Cookies["UserName"];

            // Pass session and cookie data to the view if available
            if (userName != null)
            {
                ViewBag.UserName = userName;  // Pass session data to the view
            }

            if (userCookie != null)
            {
                ViewBag.UserCookie = userCookie;  // Pass cookie data to the view
            }

            return View();
        }

        // Handle privacy page
        public IActionResult Privacy()
        {
            return View();
        }

        // Handle error page
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // Handle logout by clearing session and deleting cookies
        public IActionResult Logout()
        {
            // Clear session data
            HttpContext.Session.Clear();

            // Delete cookie
            Response.Cookies.Delete("UserName");

            return RedirectToAction("Index", "Home");  // Redirect to home page after logout
        }
    }
}
public class UserLoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; internal set; }
    }

