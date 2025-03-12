using Microsoft.AspNetCore.Identity; // ASP.NET Core Identity'i kullanabilmek için
using Microsoft.Extensions.DependencyInjection; // Bağımlılık enjeksiyonunu kullanabilmek için
using System; // Temel sistem sınıfları için
using System.Linq; // LINQ sorguları için
using System.Threading.Tasks; // Asenkron işlemler için

namespace campusakarya.Models
{
    // Identity verileri için statik veri ekleme sınıfı
    public static class IdentitySeedData
    {
        // Rollerin sabit isimleri
        private const string adminRole = "Admin";
        private const string userRole = "User";

        // Kullanıcıların sabit isimleri ve şifreleri
        private const string adminUser = "Admin";
        private const string adminPassword = "AdminPassword123$";
        private const string regularUser = "User";
        private const string regularPassword = "UserPassword123$";

        // Veritabanını kontrol edip, rolleri ve kullanıcıları ekler
        public static async Task EnsurePopulated(IServiceProvider serviceProvider)
        {
            // RoleManager ve UserManager servisini alıyoruz
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            // Admin rolü var mı kontrol et, yoksa oluştur
            if (await roleManager.FindByNameAsync(adminRole) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(adminRole)); // Admin rolünü oluştur
            }

            // User rolü var mı kontrol et, yoksa oluştur
            if (await roleManager.FindByNameAsync(userRole) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(userRole)); // User rolünü oluştur
            }

            // Admin kullanıcısını oluştur
            var admin = await userManager.FindByNameAsync(adminUser);
            if (admin == null)
            {
                // Admin kullanıcısını tanımla
                admin = new IdentityUser(adminUser)
                {
                    Email = "admin@yourapp.com",
                    UserName = adminUser,
                    PhoneNumber = "1234567890"
                };

                // Admin kullanıcısını oluştur ve admin rolünü ata
                var result = await userManager.CreateAsync(admin, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, adminRole); // Admin rolünü ekle
                }
            }

            // Regular (standart) kullanıcıyı oluştur
            var regular = await userManager.FindByNameAsync(regularUser);
            if (regular == null)
            {
                // Regular kullanıcıyı tanımla
                regular = new IdentityUser(regularUser)
                {
                    Email = "user@yourapp.com",
                    UserName = regularUser,
                    PhoneNumber = "0987654321"
                };

                // Regular kullanıcısını oluştur ve user rolünü ata
                var result = await userManager.CreateAsync(regular, regularPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(regular, userRole); // User rolünü ekle
                }
            }
        }
    }
}
