using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YSKProje.ToDo.Web.Areas.Admin.Models.AciliyetViewModels
{
    public class AciliyetUpdateViewModel
    {
        public int Id { get; set; }

        [Display(Name ="Tanım..:")]
        [Required(ErrorMessage ="Tanım Alanı Gereklidir")]
        public string Tanim { get; set; }
    }
}
