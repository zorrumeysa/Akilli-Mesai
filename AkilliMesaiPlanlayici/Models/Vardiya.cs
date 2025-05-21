// Models/Vardiya.cs
using System;
using System.ComponentModel.DataAnnotations;

namespace AkilliMesaiPlanlayici.Models
{
    public class Vardiya
    {
        public int VardiyaID { get; set; }

        [Required]
        [Display(Name = "Vardiya Adı")]
        public string VardiyaAdi { get; set; }

        [Required]
        [Display(Name = "Başlangıç Saati")]
        public TimeSpan BaslangicSaati { get; set; }

        [Required]
        [Display(Name = "Bitiş Saati")]
        public TimeSpan BitisSaati { get; set; }
    }
}
