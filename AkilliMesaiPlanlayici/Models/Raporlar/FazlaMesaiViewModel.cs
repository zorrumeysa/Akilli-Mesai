using System;
using System.ComponentModel.DataAnnotations;

namespace AkilliMesaiPlanlayici.Models.Raporlar
{
    public class FazlaMesaiViewModel
    {
        public int ID { get; set; }

        [Display(Name = "Tarih")]
        [DataType(DataType.Date)]
        public DateTime Tarih { get; set; }

        [Display(Name = "Personel")]
        public string PersonelAd { get; set; }

        [Display(Name = "Saat Sayısı")]
        public decimal SaatSayisi { get; set; }

        [Display(Name = "Neden")]
        public string? Neden { get; set; }

        [Display(Name = "Onay Durumu")]
        public bool OnayDurumu { get; set; }

        [Display(Name = "Saatlik Ücret")]
        [DataType(DataType.Currency)]
        public decimal SaatlikUcret { get; set; }

        [Display(Name = "Tutar")]
        [DataType(DataType.Currency)]
        public decimal Tutar { get; set; }

    }
}
