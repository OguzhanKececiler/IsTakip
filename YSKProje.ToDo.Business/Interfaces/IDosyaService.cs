using System;
using System.Collections.Generic;
using System.Text;

namespace YSKProje.ToDo.Business.Interfaces
{
   public interface IDosyaService
    {
        /// <summary>
        /// Geriye üretmis ve upload etmis oldugu pdf dosyasının virtual pathini döner.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        string AktarPdf<T> (List<T> list) where T:class,new(); //new() bunu koydunmu yanı newlenebılır olmak zorunda dıyorum


        /// <summary>
        /// geriye excel verisisini byte dizisi olarak döner
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        byte[] AktarExcel<T>(List<T> list) where T : class, new(); //new() bunu koydunmu yanı newlenebılır olmak zorunda dıyorum
    }
}
