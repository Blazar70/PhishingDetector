using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Threading.Tasks;

public class UploadController : Controller
{
    private readonly IWebHostEnvironment _env;
    public UploadController(IWebHostEnvironment env) { _env = env; }

    public IActionResult Index() => View();

    [HttpPost]
    public async Task<IActionResult> Save(IFormFile file)
    {
        if (file?.Length > 0)
        {
            var folder = Path.Combine(_env.WebRootPath, "uploads");
            Directory.CreateDirectory(folder);

            var name = Path.GetRandomFileName() + Path.GetExtension(file.FileName);
            var path = Path.Combine(folder, name);

            await using var fs = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(fs);
        }
        TempData["Msg"] = "File uploaded";
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Delete(string name)
    {
        var path = Path.Combine(_env.WebRootPath, "uploads", name);
        if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
        return RedirectToAction("Index");
    }
}
