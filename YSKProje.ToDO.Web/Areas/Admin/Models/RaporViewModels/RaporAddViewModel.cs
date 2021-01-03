using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.Web.Areas.Admin.Models.RaporViewModels
{
    public class RaporAddViewModel
    {
        [Display(Name ="Tanım :")]
        [Required(ErrorMessage ="Tanım alanı boş geçilemez")]
        public string Tanim{ get; set; }
        [Display(Name = "Detay:")]

        [Required(ErrorMessage = "Detay alanı boş geçilemez")]
        public string Detay { get; set; }
        public Gorev Gorev { get; set; }
        public int GorevId { get; set; }

    }
}
