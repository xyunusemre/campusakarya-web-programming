using System; // Temel sistem sınıflarını kullanabilmek için
using System.ComponentModel.DataAnnotations; // Veri doğrulama için kullanılır
using System.Linq; // LINQ sorgularını kullanabilmek için
using System.Threading.Tasks; // Asenkron işlemler için

namespace campusakarya.Models
{
    // Veritabanı verilerini başlatma ve örnek veriler ekleme işlemi yapan sınıf
    public class AppDbInitializer
    {
        // Veritabanı ilk verilerini ekleyen static metod
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            // Uygulamanın servis sağlayıcılarını kullanarak veritabanı bağlamını elde ediyoruz
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateAsyncScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                // Eğer veritabanı yoksa, oluşturulmasını sağlıyoruz
                context.Database.EnsureCreated();

                // Etkinlikler varsa eklemiyoruz, yoksa örnek etkinlikleri ekliyoruz
                if (!context.Events.Any())
                {
                    // Yeni etkinlikler listesi oluşturuyoruz
                    context.Events.AddRange(new List<Event>()
                    {
                        new Event()
                        {
                            EventName = "Konser", // Etkinlik adı
                            Description = "Üniversite öğrencileri için konser", // Etkinlik açıklaması
                            Category = EventCategory.Entertainment, // Etkinlik kategorisi
                            Date = DateTime.Now.AddDays(3), // Etkinlik tarihi (3 gün sonra)
                            Location = "Helikopter Pisti", // Etkinlik yeri
                            Image = "~/img/event-image1.jpg", // Etkinlik görseli
                            Price = "Ücretsiz", // Etkinlik fiyatı
                            PresentedBy = "SAÜ Müzik Topluluğu" // Etkinlik sunan grup
                        },

                        new Event()
                        {
                            EventName = "Tiyatro Gösterimi", // Etkinlik adı
                            Description = "Üniversite öğrencileri için tiyatro", // Etkinlik açıklaması
                            Category = EventCategory.Entertainment, // Etkinlik kategorisi
                            Date = DateTime.Now.AddDays(7), // Etkinlik tarihi (7 gün sonra)
                            Location = "İletişim Fakültesi", // Etkinlik yeri
                            Image = "~/img/event-image2.jpg", // Etkinlik görseli
                            Price = "30TL", // Etkinlik fiyatı
                            PresentedBy = "SAÜ Tiyatro Topluluğu" // Etkinlik sunan grup
                        },

                        new Event()
                        {
                            EventName = "Zaman Yönetimi Eğitimi", // Etkinlik adı
                            Description = "Üniversite öğrencileri için zaman yönetimi eğitimi", // Etkinlik açıklaması
                            Category = EventCategory.Education, // Etkinlik kategorisi
                            Date = DateTime.Now.AddDays(12), // Etkinlik tarihi (12 gün sonra)
                            Location = "Konferans Salonu", // Etkinlik yeri
                            Image = "~/img/event-image3.jpg", // Etkinlik görseli
                            Price = "Ücretsiz", // Etkinlik fiyatı
                            PresentedBy = "SAÜ Eğitim Topluluğu" // Etkinlik sunan grup
                        },

                        new Event()
                        {
                            EventName = "Mezuniyet Töreni", // Etkinlik adı
                            Description = "Üniversite 2024 Mezuniyet Töreni", // Etkinlik açıklaması
                            Category = EventCategory.Education, // Etkinlik kategorisi
                            Date = DateTime.Now.AddDays(-60), // Etkinlik tarihi (60 gün önce, geçmiş etkinlik)
                            Location = "Konferans Salonu", // Etkinlik yeri
                            Image = "~/img/event-image4.jpg", // Etkinlik görseli
                            Price = "Ücretsiz", // Etkinlik fiyatı
                            PresentedBy = "SAÜ Eğitim Kordinatörlüğü" // Etkinlik sunan grup
                        },

                        new Event()
                        {
                            EventName = "Doğa Yürüyüşü", // Etkinlik adı
                            Description = "Üniversite öğrencileri için doğa yürüyüşü", // Etkinlik açıklaması
                            Category = EventCategory.Nature, // Etkinlik kategorisi
                            Date = DateTime.Now.AddDays(4), // Etkinlik tarihi (4 gün sonra)
                            Location = "Poyrazlar Gölü", // Etkinlik yeri
                            Image = "~/img/event-image5.jpg", // Etkinlik görseli
                            Price = "Ücretsiz", // Etkinlik fiyatı
                            PresentedBy = "SAÜ Doğa Topluluğu" // Etkinlik sunan grup
                        }
                    });

                    // Yeni etkinlikleri veritabanına kaydediyoruz
                    context.SaveChanges(); 
                }
            }
        }
    }
}
