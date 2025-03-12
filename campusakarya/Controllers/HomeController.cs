using Microsoft.AspNetCore.Mvc; // Controller'ı kullanabilmek için
using System; // DateTime sınıfını kullanabilmek için
using System.Collections.Generic; // Liste kullanabilmek için
using System.Linq; // LINQ sorgularını kullanabilmek için
using Microsoft.EntityFrameworkCore; // Entity Framework Core'u kullanabilmek için
using campusakarya.Models; // AppDbContext ve Event modelini kullanabilmek için

namespace campusakarya.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context; // DbContext'i tutan değişken

        // Constructor: Dependency Injection ile AppDbContext'i alıyoruz
        public HomeController(AppDbContext context)
        {
            _context = context; // DbContext'i enjekte ediyoruz
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Etkinlikleri filtreliyoruz: Yalnızca şu anki tarih ve sonrasındaki etkinlikleri getiriyoruz
            var events = await _context.Events.Where(e => e.Date >= DateTime.Now).ToListAsync();

            // Etkinlikleri View'a gönderiyoruz
            return View(events);
        }
    }
}
