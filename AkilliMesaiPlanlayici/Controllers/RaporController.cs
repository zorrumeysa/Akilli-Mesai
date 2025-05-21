// Controllers/RaporController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AkilliMesaiPlanlayici.Data;
using AkilliMesaiPlanlayici.Models.Raporlar;
using AkilliMesaiPlanlayici.Services;
using System.Globalization;

public class RaporController : Controller
{
    private readonly ApplicationDbContext _context;
    public RaporController(ApplicationDbContext context) => _context = context;

    public IActionResult HaftalikFazlaMesai()
    {
        // Veritabanından vardiya kayıtlarını çekiyoruz
        var vardiyalar = _context.PersonelVardiyalar
            .Include(pv => pv.Personel)
            .Where(pv => pv.Personel != null && pv.GercekBitis.HasValue && pv.GercekBaslangic.HasValue)
            .ToList();

        const double standartSaat = 8.0;

        var haftalik = vardiyalar
            .GroupBy(pv => new
            {
                pv.Personel.AdSoyad,
                Yil = ISOWeek.GetYear(pv.Tarih),
                Hafta = ISOWeek.GetWeekOfYear(pv.Tarih)
            })
            .Select(g =>
            {
                // Bu gruptaki toplam fazla mesai saati
                decimal toplamFazlaSaat = g.Sum(pv =>
                {
                    // Hem GercekBitis hem GercekBaslangic artık non-null TimeSpan
                    var sure = pv.GercekBitis.Value
                                  - pv.GercekBaslangic.Value;
                    var fazlaSaat = Math.Max(0, sure.TotalHours - standartSaat);
                    return Math.Round((decimal)fazlaSaat, 2);
                });

                // Saatlik ücret
                var saatlikUcret = g.First().Personel.SaatlikUcret;

                // Toplam tutar (1.5× katsayı)
                var toplamTutar = MesaiHesaplayici.HesaplaFazlaMesai(
                    toplamFazlaSaat,
                    saatlikUcret,
                    false
                );

                return new HaftalikFazlaMesaiDto
                {
                    Yil = g.Key.Yil,
                    Hafta = g.Key.Hafta,
                    AdSoyad = g.Key.AdSoyad,
                    ToplamSaat = toplamFazlaSaat,
                    SaatlikUcret = saatlikUcret,
                    Tutar = toplamTutar
                };
            })
            .OrderByDescending(x => x.Yil)
            .ThenByDescending(x => x.Hafta)
            .ToList();

        return View(haftalik);
    }
}
    