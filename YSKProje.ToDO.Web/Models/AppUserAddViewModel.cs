using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YSKProje.ToDo.Web.Models
{
    public class AppUserAddViewModel
    {
        [Required(ErrorMessage = "Kullanıcı Adı boş geçilemez")]
        [Display(Name = "Kullanıcı Adı..:")]
        public string Username { get; set; }

        [DataType(DataType.Password)]//artıık bunun bir datatypeını belirtmis oluyoruz
        [Display(Name = "Parola..:")]
        [Required(ErrorMessage = "Şifre boş geçilemez")]
        public string Password { get; set; }

        [DataType(DataType.Password)]//artıık bunun bir datatypeını belirtmis oluyoruz
        [Display(Name = "Parolanızı Tekrar Girin..:")]
        [Compare("Password", ErrorMessage = "Şifreler uyuşmamaktadır")]
        public string ConfirmPassword { get; set; }

        [EmailAddress(ErrorMessage = "Geçersiz Email")]
        [Display(Name = "Email..:")]
        [Required(ErrorMessage = "Email boş geçilemez")]
        public string Email { get; set; }

        [Display(Name = "Ad..:")]
        [Required(ErrorMessage = "Ad boş geçilemez")]
        public string Name { get; set; }

        [Display(Name = "Soyad..:")]
        [Required(ErrorMessage = "Soyad boş geçilemez")]
        public string Surname { get; set; }
    }
}
