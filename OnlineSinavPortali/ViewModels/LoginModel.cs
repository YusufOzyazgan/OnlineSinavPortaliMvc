 using System.ComponentModel.DataAnnotations;

namespace OnlineSinavPortali.ViewModels
{
    public class LoginModel
    {
        [Display(Name = "Kullanıcı Adı Veya E Mail")]
        [Required(ErrorMessage = "Kullanıcı Adı Giriniz!")]
        public string Username{ get; set; }


        public string StudentNumber { get; set; }   



        [Display(Name = "Parola")]
        [Required(ErrorMessage = "Parola Giriniz!")]
        public string Password { get; set; }


        [Display(Name = "Beni Hatırla")]
        public bool RememberMe { get; set; }
    }
}
