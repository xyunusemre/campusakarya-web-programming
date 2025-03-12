using Microsoft.EntityFrameworkCore; // Entity Framework Core'u kullanabilmek için
using System; // Temel sistem sınıflarını kullanabilmek için
using System.ComponentModel.DataAnnotations; // Veri doğrulama için kullanılır
using System.Linq; // LINQ sorgularını kullanabilmek için
using System.Threading.Tasks; // Asenkron işlemler için

namespace campusakarya.Models
{
    // Uygulamanın veritabanı bağlantısını ve model yapılarını yöneten DbContext sınıfı
    public class AppDbContext : DbContext
    {
        // Constructor: DbContextOptions alarak base sınıfın constructor'ını çağırıyoruz
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Model oluşturma işlemleri (veritabanı tablolarının yapılandırılması)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Base sınıfın OnModelCreating metodunu çağırıyoruz
        }

        // Veritabanındaki Events tablosu ile ilişkili DbSet
        public DbSet<Event> Events { get; set; } // Event modeli ile ilişkilendirilmiş veritabanı tablosu
    }
}
