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
        public string SelectedMeasure { get => GetValue<string>(); set => SetValue(value); }
        public string AmountText { get => GetValue<string>(); set => SetValue(value); }
        public string AmountOfPrice { get => GetValue<string>(); set => SetValue(value); }
        public string SelectedCurrency { get => GetValue<string>(); set => SetValue(value); }
        public List<string> MeasureList { get => GetValue<List<string>>(); set => SetValue(value); }

        public ServicesInfo()
        {
            Index = 0;
            NameOfService = "";
            AmountText = "1";
            AmountOfPrice = "1";
            SelectedCurrency = "";
            MeasureList = GetMeasureList;

            if (MeasureList.Count > 0)
                SelectedMeasure = MeasureList[0];
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
            this.AmountOfPrice = other.AmountOfPrice;
            this.SelectedCurrency = other.SelectedCurrency;
            this.MeasureList = other.MeasureList;
            this.SelectedCurrency = other.SelectedCurrency;
        }

        public int CalcTotalCost()
        {
            int int_AmountText = string.IsNullOrEmpty(AmountText) ? 0 : int.Parse(AmountText);
            int int_AmountOfPrice = string.IsNullOrEmpty(AmountOfPrice) ? 0 : int.Parse(AmountOfPrice);

            return  (int_AmountText * int_AmountOfPrice);
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
