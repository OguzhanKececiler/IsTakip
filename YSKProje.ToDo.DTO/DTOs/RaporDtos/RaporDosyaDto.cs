using System;
using System.Collections.Generic;
using System.Text;

namespace YSKProje.ToDo.DTO.DTOs.RaporDtos
{
   public class RaporDosyaDto
    {
        public string Tanim { get; set; }
        //[Display(Name = "Detay:")]

        //[Required(ErrorMessage = "Detay alanı boş geçilemez")]
        public string Detay { get; set; }
    }
}
