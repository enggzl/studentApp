using Microsoft.EntityFrameworkCore;

namespace efcoreApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<KursKayit> KursKayitlari { get; set; }
        public DbSet<Ogrenci> Ogrenciler { get; set; }
        public DbSet<Kurs> Kurslar { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=efcoreAppDb;Trusted_Connection=True;");
            }
        }
    }
}