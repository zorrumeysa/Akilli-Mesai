using System.ComponentModel.DataAnnotations;

namespace AkilliMesaiPlanlayici.Models
{
    public class FazlaMesai
    {
        public int ID { get; set; }
        public int PersonelID { get; set; }
        public Personel Personel { get; set; }

        [Required]
        public DateTime Tarih { get; set; }

        public decimal SaatSayisi { get; set; }
        public string Neden { get; set; }

        public bool OnayDurumu { get; set; }
    }
}
