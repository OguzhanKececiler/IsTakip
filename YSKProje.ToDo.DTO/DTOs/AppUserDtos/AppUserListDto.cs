using System;
using System.Collections.Generic;
using System.Text;

namespace YSKProje.ToDo.DTO.DTOs.AppUserDtos
{
   public class AppUserListDto
    {
        public int Id { get; set; }
        //[Display(Name = "Ad :")]

        //[Required(ErrorMessage = "Ad Alanı boş geçilemez")]
        public string Name { get; set; }
        //[Display(Name = "Soyad :")]

        //[Required(ErrorMessage = "Soyad Alanı boş geçilemez")]

        public string SurName { get; set; }

        //[Display(Name = "Email  :")]
        public string Email { get; set; }
       // [Display(Name = "Resim  :")]

        public string Picture { get; set; }
    }
}
