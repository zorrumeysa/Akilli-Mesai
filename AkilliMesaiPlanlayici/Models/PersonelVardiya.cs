using System.ComponentModel.DataAnnotations;

namespace AkilliMesaiPlanlayici.Models
{
    public class PersonelVardiya
    {
        public int PersonelVardiyaID { get; set; }

        [Required]
        public int PersonelID { get; set; }
        public Personel Personel { get; set; }

        [Required]
        public int VardiyaID { get; set; }
        public Vardiya Vardiya { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Tarih { get; set; }

        [Required]
        [Display(Name = "Gerçek Başlangıç")]
        public DateTime? GercekBaslangic { get; set; }  // Nullable DateTime

        [Display(Name = "Gerçek Bitiş")]
        public DateTime? GercekBitis { get; set; }  // Nullable DateTime
    }
}
