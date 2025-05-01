using Microsoft.AspNetCore.Mvc;

public class ScanController : Controller
{
    private readonly SiteDb _db;
    public ScanController(SiteDb db) { _db = db; }

    [HttpPost]
    public IActionResult Create(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return RedirectToAction("Index", "Home");

        _db.Scans.Add(new Scan { Text = text, Scanned = DateTime.Now });
        _db.SaveChanges();

        return RedirectToAction("History", "Pages");
    }
    public IActionResult Edit(int id)
        => View(_db.Scans.Find(id));

    [HttpPost]
    public IActionResult Edit(Scan item)
    {
        if (!ModelState.IsValid) return View(item);
        _db.Update(item);
        _db.SaveChanges();
        return RedirectToAction("History", "Pages");
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        var row = _db.Scans.Find(id);
        if (row != null)
        {
            _db.Remove(row);
            _db.SaveChanges();
        }
        return RedirectToAction("History", "Pages");
    }
}
