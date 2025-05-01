using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

public class ContactController : Controller
{
    private readonly SiteDb _context;

    public ContactController(SiteDb context)
    {
        _context = context;
    }

    public IActionResult Create()
        => View(new ContactViewModel());

    [HttpPost]
    public IActionResult Create(ContactViewModel vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        var row = new Contact
        {
            Name = vm.Name,
            Email = vm.Email,
            Message = vm.Message,
            SentAt = DateTime.Now
        };

        _context.Contacts.Add(row);
        _context.SaveChanges();

        TempData["Msg"] = "Saved!";
        return RedirectToAction("Thanks");
    }

    public IActionResult Thanks() => View();

    public IActionResult Index()
    {
        var list = _context.Contacts.ToList();
        return View(list);
    }

}
