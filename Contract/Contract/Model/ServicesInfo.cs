using Contract.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Contract.Model
{
    public class ServicesInfo : BaseModel
    {
        public int Index { get => GetValue<int>(); set => SetValue(value); }
        public string NameOfService { get => GetValue<string>(); set => SetValue(value); }
        public int UnitOfMeasureIndex { get => GetValue<int>(); set => SetValue(value); }
        public string AmountText { get => GetValue<string>(); set => SetValue(value); }
        public string PriceText { get => GetValue<string>(); set => SetValue(value); }
        public List<string> MeasureList { get => GetValue<List<string>>(); set => SetValue(value); }

        public ServicesInfo()
        {
            Index = 0;
            NameOfService = "";
            UnitOfMeasureIndex = 0;
            AmountText = "1";
            PriceText = "";
            MeasureList = GetMeasureList;
        }

        public ServicesInfo(ServicesInfo other)
        {
            this.Copy(other);
        }

        public void Copy(ServicesInfo other)
        {
            this.Index = other.Index + 1;
            this.NameOfService = other.NameOfService;
            this.AmountText = other.AmountText;
            this.PriceText = other.PriceText;
            this.MeasureList = other.MeasureList;
            this.UnitOfMeasureIndex = other.UnitOfMeasureIndex;
        }

        public List<string> GetMeasureList
        {
            get
            {
                List<string> result = new List<string>();

                switch (AppSettings.GetLanguage())
                {
                    case Constant.LanUz:
                        result = ((string[])Application.Current.Resources[Constant.MeasureList_uz]).ToList();
                        break;
                    case Constant.LanUzCyrl:
                        result = ((string[])Application.Current.Resources[Constant.MeasureList_uz_cyrl]).ToList();
                        break;
                    case Constant.LanEn:
                        result = ((string[])Application.Current.Resources[Constant.MeasureList_en]).ToList();
                        break;
                    case Constant.LanRu:
                        result = ((string[])Application.Current.Resources[Constant.MeasureList_ru]).ToList();
                        break;
                }

                return result;
            }
        }
    }
}
