using Microsoft.AspNetCore.Mvc;
using AkilliMesaiPlanlayici.Data;
using AkilliMesaiPlanlayici.Models;
using System.Linq;

public class PersonelController : Controller
{
    private readonly ApplicationDbContext _context;

    public PersonelController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var list = _context.Personeller.ToList();
        return View(list);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Personel personel)
    {
        if (ModelState.IsValid)
        {
            _context.Personeller.Add(personel);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(personel);
    }

    public IActionResult Edit(int id)
    {
        var personel = _context.Personeller.Find(id);
        if (personel == null)
            return NotFound();

        return View(personel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Personel personel)
    {
        if (ModelState.IsValid)
        {
            _context.Personeller.Update(personel);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(personel);
    }

    public IActionResult Delete(int id)
    {
        var personel = _context.Personeller.Find(id);
        if (personel == null)
            return NotFound();

        _context.Personeller.Remove(personel);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
}
