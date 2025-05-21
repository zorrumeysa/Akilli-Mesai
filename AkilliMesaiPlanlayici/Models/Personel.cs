// Models/Personel.cs
using System.ComponentModel.DataAnnotations;


namespace AkilliMesaiPlanlayici.Models
{
    public class Personel
    {
        public int PersonelID { get; set; }

        [Required]
        [Display(Name = "Ad Soyad")]
        public string AdSoyad { get; set; }

        [Required]
        [Display(Name = "Sicil No")]
        public string SicilNo { get; set; }

        [Display(Name = "Birim")]
        public string? Birim { get; set; }

        public bool Durum { get; set; } = true;

        [Required]
        [Display(Name = "Aylık Maaş")]
        [DataType(DataType.Currency)]
        public decimal AylikMaas { get; set; }

        [Display(Name = "Saatlik Ücret")]
        [DataType(DataType.Currency)]
        public decimal SaatlikUcret
        {
            get
            {
                // Ayda 22 gün * 8 saat = 176 saat
                return Math.Round(AylikMaas / 176, 2);
            }
        }

        // Meslek ile ilişkiliyse foreign key
        public int MeslekID { get; set; }
        public Meslek? Meslek { get; set; }
    }
}
