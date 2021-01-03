using System;
using System.Collections.Generic;
using System.Text;

namespace YSKProje.ToDo.DTO.DTOs.AppUserDtos
{
   public class AppUserSignInDto
    {
        //[Required(ErrorMessage = "Kullanıcı Adı boş geçilemez")]
        //[Display(Name = "Kullanıcı Adı..:")]
        public string Username { get; set; }
        //[DataType(DataType.Password)]
        //[Required(ErrorMessage = "Parola boş geçilemez")]
        //[Display(Name = "Parola..:")]
        public string Password { get; set; }

        //[Display(Name = "Beni Hatırla")]
        public bool RememberMe { get; set; }
    }
}
