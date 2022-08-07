using Contract.Resources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using Xamarin.Essentials;

namespace Contract
{
    public class AppSettings
    {
        public static string Language = "Language";
        public static bool IsGooglePlayServiceAvailable { get; set; }

        public static void SetLanguage(string strLanguage)
        {
            var language = CultureInfo.GetCultures(CultureTypes.NeutralCultures).ToList().First(element => element.EnglishName.Contains(strLanguage));
            Thread.CurrentThread.CurrentUICulture = language;
            AppResource.Culture = language;

            Preferences.Set(Language, strLanguage);
        }

        public static string GetLanguage()
        {
            return Preferences.Get(Language, "");
        }

        //public static string GetLanguageCode
        //{
        //    get
        //    {
        //        string strCode = string.Empty;
        //        switch (GetLanguage())
        //        {
        //            case Constant.Uzbek: strCode = "uz"; break;
        //            case Constant.English: strCode = "en"; break;
        //            case Constant.Russian: strCode = "ru"; break;
        //            case Constant.Mongol: strCode = "mn"; break;
        //        }

        //        return strCode;
        //    }
        //}
    }
}
