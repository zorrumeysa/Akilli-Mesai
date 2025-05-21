using System;
using System.ComponentModel.DataAnnotations;

namespace AkilliMesaiPlanlayici.Models.Raporlar
{
    public class PersonelVardiyaViewModel
    {
        public int PersonelVardiyaID { get; set; }

        [Display(Name = "Ad Soyad")]
        public string AdSoyad { get; set; } = "";

        [Display(Name = "Vardiya")]
        public string VardiyaAdi { get; set; } = "";

        [Display(Name = "Tarih")]
        [DataType(DataType.Date)]
        public DateTime Tarih { get; set; }

        [Display(Name = "Gerçek Başlangıç")]
        [DataType(DataType.Time)]
        public DateTime GercekBaslangic { get; set; }

        [Display(Name = "Gerçek Bitiş")]
        [DataType(DataType.Time)]
        public DateTime GercekBitis { get; set; }

        [Display(Name = "Çalışma Saati")]
        public decimal CalismaSaati { get; set; }

        [Display(Name = "Fazla Mesai Saati")]
        public decimal FazlaMesaiSaati { get; set; }

        [Display(Name = "Fazla Mesai Ücreti")]
        [DataType(DataType.Currency)]
        public decimal FazlaMesaiUcreti { get; set; }
    }
}
