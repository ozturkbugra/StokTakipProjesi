using Microsoft.EntityFrameworkCore;
using StokTakip.Core; // Core katmanını referans aldığımız için burayı görüyor

namespace StokTakip.Repository
{
    // DbContext sınıfından miras alıyoruz (EF Core'un ana sınıfı)
    public class AppDbContext : DbContext
    {
        // Constructor: Veritabanı ayarlarını (Connection String) startup'tan alabilmek için
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Veritabanında hangi tablolar olacak?
        public DbSet<Product> Products { get; set; }
        public DbSet<Company> Companies { get; set; }

        // İlişkileri ve ayarları detaylandırdığımız yer
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Reflection: Çalışan koddaki tüm konfigürasyonları (Ayarları) bul ve uygula.
            // (İleride ayarları ayrı dosyalara yazarsak burası otomatik görecek)
            modelBuilder.ApplyConfigurationsFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}