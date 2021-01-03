using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YSKProje.ToDo.Web.Areas.Admin.Models.GorevViewModels
{
    public class GorevUpdateViewModel
    {

        public int Id{ get; set; }
        [Required(ErrorMessage = "Ad alanı gereklidir")]
        public string Ad { get; set; }

        public string Aciklama { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Lütfen bir aciliyet durumu seçiniz")]
        public int AciliyetId { get; set; }
    }
}
