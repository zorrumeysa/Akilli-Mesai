using Microsoft.EntityFrameworkCore;
using AkilliMesaiPlanlayici.Models;

namespace AkilliMesaiPlanlayici.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Personel> Personeller { get; set; }
        public DbSet<Vardiya> Vardiyalar { get; set; }
        public DbSet<PersonelVardiya> PersonelVardiyalar { get; set; }
        public DbSet<FazlaMesai> FazlaMesaier { get; set; }
    }
}
