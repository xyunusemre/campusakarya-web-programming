using Microsoft.AspNetCore.Mvc; // Controller'ı kullanabilmek için
using Microsoft.EntityFrameworkCore; // DbContext'i kullanabilmek için
using campusakarya.Models; // Event ve AppDbContext sınıflarını kullanabilmek için
using System.IO; // Dosya işlemleri için
using Microsoft.AspNetCore.Http; // Dosya yükleme işlemleri için

namespace campusakarya.Controllers
{
    public class EventsController : Controller
    {
        private readonly AppDbContext _context; // DbContext'i tutan değişken

        // Constructor: Dependency Injection ile AppDbContext'i alıyoruz
        public EventsController(AppDbContext context)
        {
            _context = context; // DbContext'i enjekte ediyoruz
        }

        [HttpGet]
        public async Task<IActionResult> Events(string category = "all", string searchTerm = "")
        {
            var events = _context.Events.AsQueryable(); // Etkinlikleri sorgulamak için başlangıç

            // Kategori filtreleme
            if (!string.IsNullOrEmpty(category) && category != "all")
            {
                if (Enum.TryParse(typeof(EventCategory), category, true, out var parsedCategory)) // Kategori enum'a dönüştürülüyor
                {
                    events = events.Where(e => e.Category == (EventCategory)parsedCategory); // Kategoriyi filtreliyoruz
                }
            }

            // Arama filtresi
            if (!string.IsNullOrEmpty(searchTerm)) // Arama terimi varsa
            {
                events = events.Where(e => e.EventName.Contains(searchTerm)); // Etkinlik adında arama yapıyoruz
            }

            // Kategorileri ViewData'ya ekle
            ViewData["Categories"] = Enum.GetValues(typeof(EventCategory)).Cast<EventCategory>().ToList(); // Kategorileri View'a gönderiyoruz
            ViewData["SelectedCategory"] = category; // Seçilen kategoriyi View'a gönderiyoruz

            return View(await events.ToListAsync()); // Etkinlikleri liste halinde döndürüyoruz
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var eventToDelete = await _context.Events.FindAsync(id); // Etkinliği buluyoruz
            if (eventToDelete != null) // Etkinlik varsa
            {
                _context.Events.Remove(eventToDelete); // Etkinliği siliyoruz
                await _context.SaveChangesAsync(); // Değişiklikleri kaydediyoruz
            }
            return RedirectToAction(nameof(Events)); // Etkinlikler sayfasına yönlendiriyoruz
        }

        [HttpGet]
        public async Task<IActionResult> EditEvent(int id)
        {
            var eventToEdit = await _context.Events.FindAsync(id); // Düzenlenecek etkinliği buluyoruz
            if (eventToEdit == null)
            {
                return NotFound(); // Etkinlik bulunamazsa hata sayfası döndürüyoruz
            }
            return View(eventToEdit); // Etkinlik düzenleme sayfasını döndürüyoruz
        }

        [HttpPost]
        public async Task<IActionResult> EditEvent(Event updatedEvent, IFormFile Image)
        {
            if (ModelState.IsValid) // Model geçerliyse
            {
                var existingEvent = await _context.Events.FindAsync(updatedEvent.EventID); // Mevcut etkinliği buluyoruz
                if (existingEvent == null)
                {
                    return NotFound(); // Etkinlik bulunamadıysa hata sayfası döndürüyoruz
                }

                // Görsel dosyasını güncelle
                if (Image != null && Image.Length > 0) // Yeni görsel varsa
                {
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", existingEvent.Image);
                    if (System.IO.File.Exists(oldImagePath)) // Eski görseli kontrol ediyoruz
                    {
                        System.IO.File.Delete(oldImagePath); // Eski görseli siliyoruz
                    }

                    var fileName = Path.GetFileName(Image.FileName); // Dosya adını alıyoruz
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", fileName); // Yeni dosya yolunu oluşturuyoruz

                    using (var stream = new FileStream(filePath, FileMode.Create)) // Yeni görseli kaydediyoruz
                    {
                        await Image.CopyToAsync(stream);
                    }

                    existingEvent.Image = "~/img/" + fileName; // Etkinlik görselini güncelliyoruz
                }
                else
                {
                    // Eğer görsel yoksa, eski görseli veya varsayılan görseli kullanıyoruz
                    existingEvent.Image = string.IsNullOrEmpty(existingEvent.Image) ? "~/img/default.jpg" : existingEvent.Image;
                }

                // Etkinlik bilgilerini güncelliyoruz
                existingEvent.EventName = updatedEvent.EventName;
                existingEvent.Description = updatedEvent.Description;
                existingEvent.Date = updatedEvent.Date;
                existingEvent.Location = updatedEvent.Location;
                existingEvent.Price = updatedEvent.Price;
                existingEvent.PresentedBy = updatedEvent.PresentedBy;
                existingEvent.Category = updatedEvent.Category;

                _context.Update(existingEvent); // Etkinliği güncelliyoruz
                await _context.SaveChangesAsync(); // Değişiklikleri kaydediyoruz

                TempData["SuccessMessage"] = "Etkinlik başarıyla güncellendi!"; // Başarı mesajı
                return RedirectToAction(nameof(Events), new { category = "all", searchTerm = "" }); // Etkinlikler sayfasına yönlendiriyoruz
            }

            return View(updatedEvent); // Model geçerli değilse, düzenleme sayfasını tekrar gösteriyoruz
        }

        public IActionResult CreateEvent()
        {
            return View(); // Yeni etkinlik oluşturma sayfasını döndürüyoruz
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(Event newEvent, IFormFile Image)
        {
            if (ModelState.IsValid) // Model geçerliyse
            {
                if (Image != null && Image.Length > 0) // Yeni görsel varsa
                {
                    var fileName = Path.GetFileName(Image.FileName); // Dosya adını alıyoruz
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", fileName); // Yeni dosya yolunu oluşturuyoruz

                    using (var stream = new FileStream(filePath, FileMode.Create)) // Yeni görseli kaydediyoruz
                    {
                        await Image.CopyToAsync(stream);
                    }

                    newEvent.Image = "~/img/" + fileName; // Etkinlik görselini güncelliyoruz
                }
                else
                {
                    newEvent.Image = "~/img/default.jpg"; // Görsel yoksa varsayılan görseli kullanıyoruz
                }

                _context.Events.Add(newEvent); // Yeni etkinliği ekliyoruz
                await _context.SaveChangesAsync(); // Değişiklikleri kaydediyoruz
                TempData["SuccessMessage"] = "Etkinlik başarıyla eklendi!"; // Başarı mesajı
                return RedirectToAction(nameof(Events)); // Etkinlikler sayfasına yönlendiriyoruz
            }

            return View(newEvent); // Model geçerli değilse, yeni etkinlik oluşturma sayfasını tekrar gösteriyoruz
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var eventDetails = await _context.Events.FirstOrDefaultAsync(e => e.EventID == id); // Etkinlik detaylarını getiriyoruz
            if (eventDetails == null)
            {
                return NotFound(); // Etkinlik bulunamadıysa hata sayfası döndürüyoruz
            }
            return View(eventDetails); // Etkinlik detaylarını gösteriyoruz
        }
    }
}
