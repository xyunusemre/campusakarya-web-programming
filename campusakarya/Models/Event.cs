using Microsoft.AspNetCore.Identity; // ASP.NET Core Identity'i kullanabilmek için
using Microsoft.Extensions.DependencyInjection; // Bağımlılık enjeksiyonu ve servisler için kullanılır
using System; // Temel sistem sınıfları ve tipler için
using System.ComponentModel.DataAnnotations; // Veri doğrulama için kullanılır
using System.Linq; // LINQ sorgularını kullanabilmek için
using System.Threading.Tasks; // Asenkron işlemler için

namespace campusakarya.Models
{
    // Etkinlik verilerini temsil eden sınıf
    public class Event
    {
        // Etkinlik ID'si - Primary Key olarak atanır
        [Key]
        public int EventID { get; set; }

        // Etkinlik adı
        public String? EventName { get; set; }

        // Etkinlik açıklaması
        public String? Description { get; set; }

        // Etkinlik kategorisi (Enum türünde)
        public EventCategory? Category { get; set; }

        // Etkinlik tarihi
        public DateTime Date { get; set; }

        // Etkinlik yeri
        public String? Location { get; set; }

        // Etkinlik görseli
        public String? Image { get; set; }

        // Etkinlik fiyatı
        public String? Price { get; set; }

        // Etkinliği düzenleyen grup
        public String? PresentedBy { get; set; }
    }
}
