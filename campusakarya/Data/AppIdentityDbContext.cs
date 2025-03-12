using Microsoft.AspNetCore.Identity; // ASP.NET Core Identity'i kullanabilmek için
using Microsoft.EntityFrameworkCore; // Entity Framework Core'u kullanabilmek için
using Microsoft.AspNetCore.Identity.EntityFrameworkCore; // Identity ile Entity Framework Core entegrasyonu için

namespace campusakarya.Models
{
    // Kullanıcı kimlik doğrulama ve yetkilendirme işlemleri için IdentityDbContext sınıfını miras alır
    public class AppIdentityDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        // Constructor: DbContextOptions alarak base sınıfın constructor'ını çağırıyoruz
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
        {
        }
    }
}
