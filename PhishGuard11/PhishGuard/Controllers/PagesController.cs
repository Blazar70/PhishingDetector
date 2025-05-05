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

    [HttpPost]
    public IActionResult Login(string user)
    {
        SetCookie("userName", user, 7);
        HttpContext.Session.SetString("loggedIn", "true");
        HttpContext.Session.SetString("userName", user);
        return RedirectToAction("Index", "Home");
    }
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        Response.Cookies.Delete("userName");
        return RedirectToAction("Login");
    }


    public IActionResult Signup() => View();
    public IActionResult History(string q)
    {
        var data = _context.Scans.AsQueryable();
        if (!string.IsNullOrWhiteSpace(q))
            data = data.Where(s => s.Text.Contains(q));
        return View(data.OrderByDescending(s => s.Scanned).ToList());
    }
    private void SetCookie(string key, string value, int days)
    {
        Response.Cookies.Append(
            key,
            value,
            new CookieOptions { Expires = DateTimeOffset.UtcNow.AddDays(days) });
    }

}
