using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YourApp.Models;      // adjust “YourApp” to your real root namespace

namespace YourApp.Controllers
{
    public class UploadController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly string _uploadDir;

        public UploadController(IWebHostEnvironment env)
        {
            _env = env;
            _uploadDir = Path.Combine(_env.WebRootPath, "uploads");
            if (!Directory.Exists(_uploadDir))
                Directory.CreateDirectory(_uploadDir);
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("loggedIn") != "true")
                return RedirectToAction("Login", "Pages");
            var vm = new UploadViewModel
            {
                FileNames = Directory
                    .GetFiles(_uploadDir)
                    .Select(Path.GetFileName)
                    .ToList(),
                Message = TempData["Msg"] as string
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Save(IFormFile file)
        {
            if (file?.Length > 0)
            {
                var filePath = Path.Combine(_uploadDir, file.FileName);
                await using var stream = System.IO.File.Create(filePath);
                await file.CopyToAsync(stream);
                TempData["Msg"] = $"Uploaded “{file.FileName}.”";
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(string name)
        {
            var full = Path.Combine(_uploadDir, name);
            if (System.IO.File.Exists(full))
                System.IO.File.Delete(full);
            TempData["Msg"] = $"Deleted “{name}.”";
            return RedirectToAction(nameof(Index));
        }
    }
}
