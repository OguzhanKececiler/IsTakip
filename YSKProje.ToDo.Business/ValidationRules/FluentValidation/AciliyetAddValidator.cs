using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.DTO.DTOs.AciliyetDtos;

namespace YSKProje.ToDo.Business.ValidationRules.FluentValidation
{
   public class AciliyetAddValidator:AbstractValidator<AciliyetAddDto>//buradan kalıtım alıp hangı class icin yapacagını buna soyluyorsun
    {
        //[Display(Name = "Tanım")]
        //[Required(ErrorMessage = "Tanım Alanı Boş Geçilemez")]
        public AciliyetAddValidator()
        {
            RuleFor(x => x.Tanim).NotNull().WithMessage("Tanım Alanı Boş Geçilemez");//kural diyosun notnull ve eger bu tercıh olmazsa bu mesajı yaz dıyorsun

        }
    }
}
