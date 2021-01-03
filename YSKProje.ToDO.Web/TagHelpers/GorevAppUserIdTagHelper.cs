using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSKProje.ToDo.Business.Interfaces;

namespace YSKProje.ToDo.Web.TagHelpers
{
    [HtmlTargetElement("getirGorevAppUserId")]
    public class GorevAppUserIdTagHelper : TagHelper
    {
        private readonly IGorevService _gorevService;

        public GorevAppUserIdTagHelper(IGorevService gorevService)
        {
            _gorevService= gorevService ;
        }
        public int AppUserId { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
         var Gorevler = _gorevService.GetirileAppUserId(AppUserId);
            int tamamlananlar = Gorevler.Where(x => x.Durum).Count();
            int UstundeCalisilanGorevSayisi = Gorevler.Where(x => !x.Durum).Count();

            string htmlstring = $"<strong>Tamamladığı görev sayısı: </strong> {tamamlananlar} <br/> <strong> Üstünde çalıştıgı görev sayısı: </strong> {UstundeCalisilanGorevSayisi}";
            output.Content.SetHtmlContent(htmlstring);
            base.Process(context, output);
        }
    }
}
