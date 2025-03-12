using System.Net.Http.Headers;
using campusakarya.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

public class AccountController : Controller
{
    private readonly SignInManager<IdentityUser> _signInManager; // Giriş işlemleri için kullanılan SignInManager
    private readonly UserManager<IdentityUser> _userManager; // Kullanıcı yönetimi için kullanılan UserManager

    public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
    {
        _signInManager = signInManager; // SignInManager'ı enjekte ediyoruz
        _userManager = userManager; // UserManager'ı enjekte ediyoruz
    }

    [HttpGet]
    public IActionResult Login(string ReturnUrl)
    {
        // ReturnUrl, kullanıcı giriş yaptıktan sonra geri dönmek istediğimiz URL'yi tutar
        ViewData["ReturnUrl"] = ReturnUrl; // ReturnUrl'yi ViewData'ya ekliyoruz
        return View(); // Login view'ını döndürüyoruz
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model, string ReturnUrl)
    {
        // Giriş işlemi yapılacak. Kullanıcı adı ve şifre kontrolü burada yapılır
        var user = await _userManager.FindByNameAsync(model.UserName); // Kullanıcı adı ile kullanıcıyı buluyoruz

        if (user != null) // Kullanıcı bulunduysa
        {
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false); // Şifre kontrolü yapılıyor

            if (result.Succeeded) // Giriş başarılıysa
            {
                return RedirectToAction("Index", "Home"); // Ana sayfaya yönlendiriyoruz
            }
            else
            {
                TempData["ErrorMessage"] = "Giriş başarısız! Kullanıcı adı veya şifre yanlış."; // Hata mesajı
            }
        }
        else
        {
            TempData["ErrorMessage"] = "Giriş başarısız! Kullanıcı adı veya şifre yanlış."; // Hata mesajı
        }

        // Hata varsa login formunu tekrar gösteriyoruz
        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync(); // Kullanıcıyı çıkartıyoruz
        return RedirectToAction("Login", "Account"); // Login sayfasına yönlendiriyoruz
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View(); // Kayıt sayfasını döndürüyoruz
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid) // Model doğrulaması geçerse
        {
            var user = new IdentityUser
            {
                UserName = model.UserName, // Kullanıcı adı
                Email = model.Email // Kullanıcı e-posta adresi
            };

            var result = await _userManager.CreateAsync(user, model.Password); // Kullanıcıyı oluşturuyoruz

            if (result.Succeeded) // Eğer kullanıcı başarılı bir şekilde oluşturulursa
            {
                await _userManager.AddToRoleAsync(user, "User"); // Kullanıcıya 'User' rolünü ekliyoruz
                return RedirectToAction("Index", "Home"); // Ana sayfaya yönlendiriyoruz
            }

            foreach (var error in result.Errors) // Hata varsa, ModelState'e ekliyoruz
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return View(model); // Model geçerli değilse, kayıt formunu tekrar döndürüyoruz
    }
}
