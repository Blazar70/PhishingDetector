using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


public class PagesController : Controller
{
    private readonly SiteDb _context;
    public PagesController(SiteDb context) { _context = context; }
    public IActionResult About() => View();
    public IActionResult Help() => View();
    public IActionResult Resources() => View();
    public IActionResult Login() => View();
    public IActionResult Signup() => View();
    public IActionResult History(string q)
    {
        var data = _context.Scans.AsQueryable();
        if (!string.IsNullOrWhiteSpace(q))
            data = data.Where(s => s.Text.Contains(q));
        return View(data.OrderByDescending(s => s.Scanned).ToList());
    }

}
