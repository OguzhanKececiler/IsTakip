using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YSKProje.ToDo.Web.Areas.Admin.Models.AciliyetViewModels
{
    public class AciliyetAddViewModel
    {
        [Display(Name="Tanım")]
        [Required(ErrorMessage ="Tanım Alanı Boş Geçilemez")]
        public string  Tanim { get; set; }
    }
}
