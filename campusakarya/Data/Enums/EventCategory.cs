using System; // Temel sistem sınıflarını kullanabilmek için
using System.ComponentModel.DataAnnotations; // Veri doğrulama için kullanılır
using System.Linq; // LINQ sorgularını kullanabilmek için
using System.Threading.Tasks; // Asenkron işlemler için

namespace campusakarya.Models
{
    // Etkinlik kategorilerini tanımlayan enum
    public enum EventCategory
    {
        Education = 1, // Eğitim kategorisi
        Nature = 2, // Doğa kategorisi
        Entertainment = 3 // Eğlence kategorisi
    }
}
