using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AkilliMesaiPlanlayici.Data;
using AkilliMesaiPlanlayici.Models;
using AkilliMesaiPlanlayici.Models.Raporlar;
using AkilliMesaiPlanlayici.Services;

namespace AkilliMesaiPlanlayici.Controllers
{
    public class PersonelVardiyaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonelVardiyaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PersonelVardiya
        public IActionResult Index()
        {
            const double standartSaat = 8.0;

            var liste = _context.PersonelVardiyalar
                .Include(pv => pv.Personel)
                .Include(pv => pv.Vardiya)
                .Where(pv => pv.GercekBaslangic.HasValue && pv.GercekBitis.HasValue)
                .AsEnumerable()    // ← Burada EF Core sorgusu bitiyor
                .Select(pv =>      // ← Bundan sonrası normal LINQ to Objects
                {
                    // 1) Çalışma süresi
                    var sure = pv.GercekBitis.Value - pv.GercekBaslangic.Value;
                    var toplamSaat = sure.TotalHours;
                    var fazlaSaat = Math.Max(0, toplamSaat - standartSaat);

                    // 2) Saatlik ücret
                    var saatlikUcret = pv.Personel.SaatlikUcret;

                    // 3) Fazla mesai ücreti (1.5× katsayı)
                    var fazlaMesaiUcreti = MesaiHesaplayici.HesaplaFazlaMesai(
                        Math.Round((decimal)fazlaSaat, 2),
                        saatlikUcret,
                        false
                    );

                    return new PersonelVardiyaViewModel
                    {
                        PersonelVardiyaID = pv.PersonelVardiyaID,
                        AdSoyad = pv.Personel.AdSoyad,
                        VardiyaAdi = pv.Vardiya.VardiyaAdi,
                        Tarih = pv.Tarih,
                        GercekBaslangic = pv.GercekBaslangic.Value,
                        GercekBitis = pv.GercekBitis.Value,
                        CalismaSaati = Math.Round((decimal)toplamSaat, 2),
                        FazlaMesaiSaati = Math.Round((decimal)fazlaSaat, 2),
                        FazlaMesaiUcreti = fazlaMesaiUcreti
                    };
                })
                .ToList();

            return View(liste);
        }


        // GET: PersonelVardiya/Create
        public IActionResult Create()
        {
            ViewBag.Personeller = _context.Personeller.Where(p => p.Durum).ToList();
            ViewBag.Vardiyalar = _context.Vardiyalar.ToList();
            return View();
        }

        // POST: PersonelVardiya/Create
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(PersonelVardiya model)
        {
            if (ModelState.IsValid)
            {
                _context.PersonelVardiyalar.Add(model);
                _context.SaveChanges();

                const double standartSaat = 8.0;
                var sure = model.GercekBitis.Value - model.GercekBaslangic.Value;
                var toplamSaat = sure.TotalHours;
                var fazlaSaat = Math.Max(0, toplamSaat - standartSaat);

                var saatlikUcret = model.Personel!.SaatlikUcret;
                var fazlaMesaiUcreti = MesaiHesaplayici.HesaplaFazlaMesai(
                    Math.Round((decimal)fazlaSaat, 2),
                    saatlikUcret
                );

                if (fazlaSaat > 0)
                {
                    var fm = new FazlaMesai
                    {
                        PersonelID = model.PersonelID,
                        Tarih = model.Tarih,
                        SaatSayisi = Math.Round((decimal)fazlaSaat, 2),
                        Neden = "Günlük 8 saat üzeri çalışma",
                        OnayDurumu = false
                    };
                    _context.FazlaMesaier.Add(fm);
                    _context.SaveChanges();
                }

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Personeller = _context.Personeller.Where(p => p.Durum).ToList();
            ViewBag.Vardiyalar = _context.Vardiyalar.ToList();
            return View(model);
        }
    }
}
