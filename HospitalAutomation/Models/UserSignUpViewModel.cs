using System.ComponentModel.DataAnnotations;

namespace HospitalAutomation.Models
{
    public class UserSignUpViewModel
    {
        [Required(ErrorMessage="Lütfen ad soyad giriniz")]
        public string NameSurname { get; set; }
        [Required(ErrorMessage = "Lütfen hakkınızda kısmına bilgi giriniz")]
        public string About { get; set; }
        [Required(ErrorMessage = "Lütfen şifre giriniz")]
        public string Password { get; set; }
        [Compare("Password",ErrorMessage = "Şifreler uyuşmuyor")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Lütfen mail giriniz")]
        public string Mail { get; set; }
        [Required(ErrorMessage = "Lütfen kullanıcı adı giriniz")]
        public string UserName { get; set; }
    }
}
