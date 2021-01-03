using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YSKProje.ToDo.Web.Models
{
    public class AppUserSignInModel
    {
        [Required(ErrorMessage ="Kullanıcı Adı boş geçilemez")]
        [Display(Name ="Kullanıcı Adı..:")]
        public string  Username { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Parola boş geçilemez")]
        [Display(Name = "Parola..:")]
        public string Password { get; set; }

        [Display(Name ="Beni Hatırla")]
        public bool RememberMe { get; set; }
    }
}
