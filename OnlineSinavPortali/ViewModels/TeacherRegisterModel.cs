using System.ComponentModel.DataAnnotations;

namespace OnlineSinavPortali.ViewModels
{
    public class TeacherRegisterModel
    {
        
        [Required(ErrorMessage = "Kullanıcı Adı Zorunludur!")]
        [Display(Name = "Kullanıcı Adı")]
        public string Username { get; set; }


        [Display(Name = "İsim Soyisim")]
        [Required(ErrorMessage = "İsim Soyisim Zorunludur!")]
        public string FullName { get; set; }


        [Display(Name = "Okul E Mail Adresi")]
        [Required(ErrorMessage = "E-Posta Adresi Zorunludur!")]
        [EmailAddress(ErrorMessage = "Geçerli bir E-Posta Adresi Giriniz!")]
        public string Email { get; set; }



        [Display(Name = "Parola")]
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{8,}$", ErrorMessage = "Şifre en az bir küçük harf,bir büyük harf,bir rakam içermeli ve 8 karakterden uzun olmalı.Türkçe karakter olmamalı...")]
        [Required(ErrorMessage = "Parola Giriniz!")]
        public string Password { get; set; }



        [Display(Name = "Parola Tekrarı")]
        [Required(ErrorMessage = "Parola Tekrar Giriniz!")]
        [Compare("Password", ErrorMessage = "Parola Tekrarı Tutarsızdır!")]
        public string PasswordConfirm { get; set; }






        //[Display(Name = "Fotograf")]
        //public IFormFile? PhotoFile { get; set; }
    }

}
