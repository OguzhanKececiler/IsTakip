using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.DTO.DTOs.AppUserDtos;

namespace YSKProje.ToDo.Business.ValidationRules.FluentValidation
{
   public class AppUserSignInValidator : AbstractValidator<AppUserSignInDto>
    {
        public AppUserSignInValidator()
        {
            RuleFor(x => x.Username).NotNull().WithMessage("Kullanıcı Adı boş geçilemez");
            RuleFor(x => x.Password).NotNull().WithMessage("Şifre Alanı boş geçilemez");
        }
    }
}
