using Microsoft.EntityFrameworkCore;

namespace Gunluk.Data
{
    public class UygulamaDbContext : DbContext
    {
        public UygulamaDbContext(DbContextOptions<UygulamaDbContext> options) : base(options)
        {

        }

        public DbSet<Kategori> Kategoriler => Set<Kategori>();
        public DbSet<Gonderi> Gonderiler => Set<Gonderi>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Kategori>().HasData(
                new Kategori() { Id = 1, Ad = "Genel" },
                new Kategori() { Id = 2, Ad = "Gezilerim" },
                new Kategori() { Id = 3, Ad = "Mutfağım" },
                new Kategori() { Id = 4, Ad = "İş Dünyam" }
                );
            modelBuilder.Entity<Gonderi>().HasData(OrnekGonderiler());
        }

        private Gonderi[] OrnekGonderiler()
        {
            var gonderiler = new Gonderi[48];

            for (int i = 0; i < gonderiler.Length; i++)
            {
                gonderiler[i] = new Gonderi()
                {
                    Id = i + 1,
                    Baslik = $"Örnek Yazı {i + 1}",
                    Icerik = @"
                    <p>Tincidunt integer eu augue augue nunc elit dolor, luctus placerat scelerisque euismod, iaculis eu lacus nunc mi elit, vehicula ut laoreet ac, aliquam sit amet justo nunc tempor, metus vel.</p>
                    <p>Placerat suscipit, orci nisl iaculis eros, a tincidunt nisi odio eget lorem nulla condimentum tempor mattis ut vitae feugiat augue cras ut metus a risus iaculis scelerisque eu ac ante.</p>",
                    KategoriId = i / 12 + 1,
                };
            }

            return gonderiler;
        }
    }
}
