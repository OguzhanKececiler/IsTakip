using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSKProje.ToDo.DTO.DTOs.AciliyetDtos;
using YSKProje.ToDo.DTO.DTOs.AppUserDtos;
using YSKProje.ToDo.DTO.DTOs.BildirimDtos;
using YSKProje.ToDo.DTO.DTOs.GorevDtos;
using YSKProje.ToDo.DTO.DTOs.RaporDtos;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.Web.Mapping.AutoMapperProfile
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            //tamamını sec ctrl+k+s basarsan alıp ona region olusturur
            #region Aciliyet-aciliyetdto

            //ve maplemesi icin 2 tarafında maplenecek kısımlarının aynı isimde ve aynı tipte olmak zorunda
            CreateMap<AciliyetAddDto, Aciliyet>();//ilk yazdıgını 2.yazdıgına ceviririm diyor birbirlerine ceviriyoruz
            CreateMap<Aciliyet, AciliyetAddDto>();//ilk yazdıgını 2.yazdıgına ceviririm diyor

            CreateMap<AciliyetListDto, Aciliyet>();
            CreateMap<Aciliyet, AciliyetListDto>();

            CreateMap<AciliyetUpdateDto, Aciliyet>();
            CreateMap<Aciliyet, AciliyetUpdateDto>();
            #endregion

            #region appuser-appuserdto
            CreateMap<AppUserAddDto, AppUser>();
            CreateMap<AppUser, AppUserAddDto>();

            CreateMap<AppUserListDto, AppUser>();
            CreateMap<AppUser, AppUserListDto>();

            CreateMap<AppUserSignInDto, AppUser>();
            CreateMap<AppUser, AppUserSignInDto>();
            #endregion

            #region bildirim-bildirimdto
            CreateMap<BildirimListDto, Bildirim>();
            CreateMap<Bildirim, BildirimListDto>();
            #endregion

            #region Gorev-GorevDto

            CreateMap<GorevAddDto, Gorev>();
            CreateMap<Gorev, GorevAddDto>();


            CreateMap<GorevListAllDto, Gorev>();
            CreateMap<Gorev, GorevListAllDto>();

            CreateMap<GorevListDto, Gorev>();
            CreateMap<Gorev, GorevListDto>();

            CreateMap<GorevUpdateDto, Gorev>();
            CreateMap<Gorev, GorevUpdateDto>();
            #endregion


            #region rapordto-rapor
            CreateMap<RaporAddDto, Rapor>();
            CreateMap<Rapor, RaporAddDto>();

            CreateMap<RaporUpdateDto, Rapor>();
            CreateMap<Rapor, RaporUpdateDto>();
            CreateMap<RaporDosyaDto, Rapor>();
            CreateMap<Rapor, RaporDosyaDto>();

            #endregion

            #region gorevlistall-Gorev
            CreateMap<GorevListAllDto, Gorev>();
            CreateMap<Gorev, GorevListAllDto>(); 
            #endregion

        }
    }
}
