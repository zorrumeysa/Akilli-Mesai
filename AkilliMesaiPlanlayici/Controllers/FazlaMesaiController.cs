using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AkilliMesaiPlanlayici.Data;
using AkilliMesaiPlanlayici.Models;
using System;
using System.IO;
using System.Linq;
using AkilliMesaiPlanlayici.Services;
using AkilliMesaiPlanlayici.Models.Raporlar; 


public class FazlaMesaiController : Controller
{
    private readonly ApplicationDbContext _context;

    public FazlaMesaiController(ApplicationDbContext context)
    {
        _context = context;
    }


    // GET: FazlaMesai
    public IActionResult Index(string baslangic, string bitis)
    {
        var query = _context.FazlaMesaier
            .Include(f => f.Personel)
            .AsQueryable();

        if (!string.IsNullOrEmpty(baslangic))
        {
            var startDate = DateTime.Parse(baslangic);
            query = query.Where(f => f.Tarih >= startDate);
        }

        if (!string.IsNullOrEmpty(bitis))
        {
            var endDate = DateTime.Parse(bitis);
            query = query.Where(f => f.Tarih <= endDate);
        }

        var list = query
            .OrderByDescending(f => f.Tarih)
            .ToList(); // Direkt FazlaMesai listesi

        ViewBag.Baslangic = baslangic;
        ViewBag.Bitis = bitis;

        return View(list); // Artık FazlaMesai listesi döndürülüyor
    }




    // GET: FazlaMesai/Create
    public IActionResult Create()
    {
        ViewBag.Personeller = _context.Personeller.Where(p => p.Durum).ToList();
        return View();
    }

    // POST: FazlaMesai/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(FazlaMesai model)
    {
        if (ModelState.IsValid)
        {
            _context.FazlaMesaier.Add(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        ViewBag.Personeller = _context.Personeller.Where(p => p.Durum).ToList();
        return View(model);
    }

    // GET: FazlaMesai/Edit/5
    public IActionResult Edit(int id)
    {
        var fazlaMesai = _context.FazlaMesaier.Find(id);
        if (fazlaMesai == null)
        {
            return NotFound();
        }

        ViewBag.Personeller = _context.Personeller.Where(p => p.Durum).ToList();
        return View(fazlaMesai);
    }

    // POST: FazlaMesai/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, FazlaMesai model)
    {
        if (id != model.ID)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(model);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.FazlaMesaier.Any(e => e.ID == model.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Personeller = _context.Personeller.Where(p => p.Durum).ToList();
        return View(model);
    }

    // GET: FazlaMesai/Delete/5
    public IActionResult Delete(int id)
    {
        var fazlaMesai = _context.FazlaMesaier.Find(id);
        if (fazlaMesai == null)
        {
            return NotFound();
        }

        return View(fazlaMesai);
    }

    // POST: FazlaMesai/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var fazlaMesai = _context.FazlaMesaier.Find(id);
        if (fazlaMesai == null)
        {
            return NotFound();
        }

        _context.FazlaMesaier.Remove(fazlaMesai);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index)); // Silme işleminden sonra Index sayfasına yönlendir
    }

    // GET: FazlaMesai/ExportToExcel
    [HttpGet]
    public IActionResult ExportToExcel(string baslangic, string bitis)
    {
        var query = _context.FazlaMesaier.Include(x => x.Personel).AsQueryable();

        // Tarih filtresi ekleyin
        if (!string.IsNullOrEmpty(baslangic))
        {
            var startDate = DateTime.Parse(baslangic);
            query = query.Where(x => x.Tarih >= startDate);
        }

        if (!string.IsNullOrEmpty(bitis))
        {
            var endDate = DateTime.Parse(bitis);
            query = query.Where(x => x.Tarih <= endDate);
        }

        var kayitlar = query.OrderByDescending(x => x.Tarih).ToList();

        using var workbook = new ClosedXML.Excel.XLWorkbook();
        var sheet = workbook.Worksheets.Add("FazlaMesai");

        // Başlıklar
        sheet.Cell(1, 1).Value = "Tarih";
        sheet.Cell(1, 2).Value = "Personel";
        sheet.Cell(1, 3).Value = "Saat";
        sheet.Cell(1, 4).Value = "Neden";
        sheet.Cell(1, 5).Value = "Durum";
        sheet.Cell(1, 6).Value = "Saatlik Ücret"; // Yeni Kolon ekledik

        int row = 2;

        foreach (var item in kayitlar)
        {
            sheet.Cell(row, 1).Value = item.Tarih.ToString("yyyy-MM-dd");
            sheet.Cell(row, 2).Value = item.Personel?.AdSoyad;
            sheet.Cell(row, 3).Value = item.SaatSayisi;
            sheet.Cell(row, 4).Value = item.Neden;
            sheet.Cell(row, 5).Value = item.OnayDurumu ? "Onaylı" : "Bekliyor";
            sheet.Cell(row, 6).Value = item.Personel?.SaatlikUcret; // Saatlik Ücret değerini ekliyoruz
            row++;
        }

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        stream.Seek(0, SeekOrigin.Begin);

        return File(stream.ToArray(),
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            "FazlaMesaiRaporu.xlsx");
    }

}
