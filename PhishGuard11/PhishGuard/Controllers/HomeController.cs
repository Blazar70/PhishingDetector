using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PhishGuard.Models;

namespace PhishGuard.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("id")))
                HttpContext.Session.SetString("id", Guid.NewGuid().ToString());

            var user = Request.Cookies["userName"] ?? "guest";
            HttpContext.Session.SetString("username", user);

            return View();
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
