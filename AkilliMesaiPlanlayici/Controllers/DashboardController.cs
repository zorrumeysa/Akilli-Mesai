using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AkilliMesaiPlanlayici.Data;
using AkilliMesaiPlanlayici.Models;

public class DashboardController : Controller
{
    private readonly ApplicationDbContext _context;

    public DashboardController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var toplamPersonel = _context.Personeller.Count();
        var toplamFazlaMesai = _context.FazlaMesaier.Sum(f => (decimal?)f.SaatSayisi) ?? 0;
        var bekleyenMesai = _context.FazlaMesaier.Count(f => !f.OnayDurumu);

        var enCokYapan = _context.FazlaMesaier
            .Include(f => f.Personel)
            .GroupBy(f => f.Personel.AdSoyad)
            .Select(g => new
            {
                AdSoyad = g.Key,
                ToplamSaat = g.Sum(f => f.SaatSayisi)
            })
            .OrderByDescending(x => x.ToplamSaat)
            .Take(5)
            .ToList();

        ViewBag.ToplamPersonel = toplamPersonel;
        ViewBag.ToplamSaat = toplamFazlaMesai;
        ViewBag.BekleyenMesai = bekleyenMesai;
        ViewBag.EnCokYapanlar = enCokYapan;

        return View();
    }
}
