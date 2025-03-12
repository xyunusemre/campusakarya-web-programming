using System.ComponentModel.DataAnnotations; // Veri doğrulama için gerekli olan sınıfları kullanabilmek için

namespace campusakarya.ViewModels
{
    // Kullanıcı kayıt işlemi için kullanılan ViewModel sınıfı
    public class RegisterViewModel
    {
        // Kullanıcı adı alanı: Zorunlu ve "Username" olarak görüntülenecek
        [Required] // Bu alanın zorunlu olduğunu belirtir
        [Display(Name = "Username")] // Görünümde "Username" olarak görüntülenmesini sağlar
        public string UserName { get; set; }

        // E-posta alanı: Zorunlu, e-posta formatı doğrulaması yapacak ve "Email" olarak görüntülenecek
        [Required] // Bu alanın zorunlu olduğunu belirtir
        [EmailAddress] // Bu alanın e-posta formatında olmasını sağlar
        [Display(Name = "Email")] // Görünümde "Email" olarak görüntülenmesini sağlar
        public string Email { get; set; }

        // Şifre alanı: Zorunlu, parola türünde olacak ve "Password" olarak görüntülenecek
        [Required] // Bu alanın zorunlu olduğunu belirtir
        [DataType(DataType.Password)] // Bu alanın şifre türü olduğunu belirtir
        [Display(Name = "Password")] // Görünümde "Password" olarak görüntülenmesini sağlar
        public string Password { get; set; }

        // Şifreyi onaylama alanı: Zorunlu, parola türü olacak, şifreyle eşleşmesi gereken ve "Confirm Password" olarak görüntülenecek
        [Required] // Bu alanın zorunlu olduğunu belirtir
        [DataType(DataType.Password)] // Bu alanın şifre türü olduğunu belirtir
        [Display(Name = "Confirm Password")] // Görünümde "Confirm Password" olarak görüntülenmesini sağlar
        [Compare("Password", ErrorMessage = "Passwords do not match.")] // Şifrenin doğruluğunu kontrol eder, eşleşmezse hata mesajı gösterir
        public string ConfirmPassword { get; set; }
    }
}
