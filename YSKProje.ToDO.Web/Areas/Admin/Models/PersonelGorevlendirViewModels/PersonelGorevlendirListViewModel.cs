using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using YSKProje.ToDo.Entities.Concrete;
using YSKProje.ToDo.Web.Areas.Admin.Models.AppUserViewModels;
using YSKProje.ToDo.Web.Areas.Admin.Models.GorevViewModels;

namespace YSKProje.ToDo.Web.Areas.Admin.Models.PersonelGorevlendirViewModels
{
    public class PersonelGorevlendirListViewModel
    {
        public AppUserListViewModel AppUser { get; set; }
        public GorevListViewModel Gorev { get; set; }
    }
}
