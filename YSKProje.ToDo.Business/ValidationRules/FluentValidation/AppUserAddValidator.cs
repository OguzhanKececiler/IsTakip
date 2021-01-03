using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.DTO.DTOs.AppUserDtos;

namespace YSKProje.ToDo.Business.ValidationRules.FluentValidation
{
  public  class AppUserAddValidator: AbstractValidator<AppUserAddDto>
    {
        public AppUserAddValidator()
        {
            RuleFor(x => x.Username).NotNull().WithMessage("Kullanıcı adı boş geçilemez");
            RuleFor(x => x.Password).NotNull().WithMessage("Parola alanı boş geçilemez");
            RuleFor(x => x.ConfirmPassword).NotNull().WithMessage("Parola onayı boş geçilemez");
            RuleFor(x => x.ConfirmPassword).Equal(x => x.ConfirmPassword).WithMessage("Parolarınız eşleşmiyor");
            RuleFor(x => x.Email).NotNull().EmailAddress().WithMessage("Geçersiz email adresi");
            RuleFor(x => x.Name).NotNull().WithMessage("Ad alanı boş geçilemez");
            RuleFor(x => x.Surname).NotNull().WithMessage("Soyad alanı boş geçilemez");
        }
    }
}
