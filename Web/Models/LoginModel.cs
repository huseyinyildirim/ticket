using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Web.Models
{
    public class LoginModel
    {
        [Display(Name = "Kullanıcı Adı / E-Posta")]
        [Required(ErrorMessage = "Kullanıcı Adı boş geçilemez.")]
        public string USERNAME { get; set; }

        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "Şifre alanı boş geçilemez.")]
        public string PASSWORD { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string GO_BACK { get; set; }
    }
}