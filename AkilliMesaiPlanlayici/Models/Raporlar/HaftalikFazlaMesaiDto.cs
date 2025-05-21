// Models/Raporlar/HaftalikFazlaMesaiDto.cs
using System.ComponentModel.DataAnnotations;

namespace AkilliMesaiPlanlayici.Models.Raporlar
{
    public class HaftalikFazlaMesaiDto
    {
        [Display(Name = "Yıl")]
        public int Yil { get; set; }

        [Display(Name = "Hafta")]
        public int Hafta { get; set; }

        // Personel AdSoyad Eklendi
        [Display(Name = "Personel")]
        public string AdSoyad { get; set; }

        [Display(Name = "Toplam Saat")]
        public decimal ToplamSaat { get; set; }

        [Display(Name = "Saatlik Ücret")]
        [DataType(DataType.Currency)]
        public decimal SaatlikUcret { get; set; }

        [Display(Name = "Tutar")]
        [DataType(DataType.Currency)]
        public decimal Tutar { get; set; }  // artık hem get hem set var
    }
}
