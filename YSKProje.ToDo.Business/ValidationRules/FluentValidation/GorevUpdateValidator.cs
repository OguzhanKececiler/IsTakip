using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.DTO.DTOs.GorevDtos;

namespace YSKProje.ToDo.Business.ValidationRules.FluentValidation
{
   public class GorevUpdateValidator : AbstractValidator<GorevUpdateDto>
    {
        public GorevUpdateValidator()
        {
            RuleFor(x => x.Ad).NotNull().WithMessage("Ad alanı gereklidir");
            RuleFor(x => x.AciliyetId).ExclusiveBetween( 0,int.MaxValue).WithMessage("Lütfen bir aciliyet durumu seçiniz");//exclusivebetween bu degerler arasında olmalı diyorum
        }
       
    }
}
