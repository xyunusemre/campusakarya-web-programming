using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using campusakarya.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("database")));  // AppDbContext veritabanı bağlantısı için SQLite kullanılıyor.

builder.Services.AddDbContext<AppIdentityDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("IdentityDbConnection")));  // AppIdentityDbContext için SQLite bağlantısı ekleniyor (Identity için).

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddRoles<IdentityRole>()  // Kullanıcı rolleri ekleniyor.
    .AddEntityFrameworkStores<AppIdentityDbContext>()  // Identity veritabanı işlemleri için EF Core kullanılacak.
    .AddDefaultTokenProviders();  // Varsayılan token sağlayıcıları ekleniyor (şifre sıfırlama vb. için).

builder.Services.AddControllersWithViews();  // MVC controller ve view desteği ekleniyor.

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();  // Geliştirme ortamında ayrıntılı hata sayfası gösterilecek.
}
else
{
    app.UseExceptionHandler("/Home/Error");  // Üretim ortamında genel hata sayfasına yönlendirme.
    app.UseHsts();  // HTTP Strict Transport Security (HSTS) kullanılarak güvenli bağlantı zorunlu hale getiriliyor.
}

app.UseHttpsRedirection();  // HTTP'den HTTPS'ye yönlendirme.
app.UseStaticFiles();  // Statik dosyaların sunulması.
app.UseRouting();  // Yönlendirme işlemlerini başlatıyor.

app.UseAuthentication(); // Authentication middleware: Kimlik doğrulama işlemi yapılacak.
app.UseAuthorization();  // Authorization middleware: Kullanıcı yetkilendirme işlemleri yapılacak.

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");  // Varsayılan rota, Home controller'ı ve Index action'ı ile başlatılacak.


// Seed işlemi
using (var scope = app.Services.CreateScope())  // Uygulamanın başlangıcında veritabanı oluşturulup veriler ekleniyor.
{
    var services = scope.ServiceProvider;
    try
    {
        await IdentitySeedData.EnsurePopulated(services);  // Identity seed verileri oluşturuluyor.
        AppDbInitializer.Seed(app);  // Diğer veritabanı seed işlemi burada yapılabilir.
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred during seeding: {ex.Message}");  // Seed işlemi sırasında hata oluşursa konsola yazdırılır.
    }
}

app.Run();  // Uygulama başlatılır.
