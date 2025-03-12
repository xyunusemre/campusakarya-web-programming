using System.ComponentModel.DataAnnotations; // Veri doğrulama için gerekli olan sınıfları kullanabilmek için

namespace campusakarya.ViewModels
{
    // Kullanıcı girişini temsil eden ViewModel sınıfı
    public class LoginViewModel
    {
        // Kullanıcı adı alanı: Zorunlu ve "Username" olarak görüntülenecek
        [Required] // Bu alanın zorunlu olduğunu belirtir
        [Display(Name = "Username")] // Görünümde "Username" olarak görüntülenmesini sağlar
        public string UserName { get; set; }

        // Şifre alanı: Zorunlu, parola türü ve "Password" olarak görüntülenecek
        [Required] // Bu alanın zorunlu olduğunu belirtir
        [DataType(DataType.Password)] // Bu alanın şifre türü olduğunu belirtir
        [Display(Name = "Password")] // Görünümde "Password" olarak görüntülenmesini sağlar
        public string Password { get; set; }
    }
}
