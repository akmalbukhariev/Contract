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
        public string TypedMeasure { get => GetValue<string>(); set => SetValue(value); }
        public bool ShowTypeMeasure { get => GetValue<bool>(); set => SetValue(value); }
        public int SelectedMeasure_index { get => GetValue<int>(); set => SetValue(value); }
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
            SelectedMeasure = "";
            SelectedMeasure_index = 0;
            TypedMeasure = "";
            ShowTypeMeasure = false;

            if (MeasureList.Count > 0)
                SelectedMeasure = MeasureList[0];
        }

        public ServicesInfo(ServicesInfo other)
        {
            this.Copy(other);
        }

        public void Copy(ServicesInfo other)
        {
            Index = other.Index + 1;
            NameOfService = other.NameOfService;
            AmountText = other.AmountText;
            AmountOfPrice = other.AmountOfPrice;
            SelectedCurrency = other.SelectedCurrency;
            MeasureList = other.MeasureList;
            SelectedMeasure = other.SelectedMeasure;
            SelectedMeasure_index = other.SelectedMeasure_index;
            TypedMeasure = other.TypedMeasure;
            ShowTypeMeasure = other.ShowTypeMeasure;
        }

        public double CalcTotalCost()
        {
            double int_AmountText = 0.0;
            double int_AmountOfPrice = 0.0;

            try
            {
                int_AmountText = string.IsNullOrEmpty(AmountText) ? 0 : double.Parse(AmountText);
                int_AmountOfPrice = string.IsNullOrEmpty(AmountOfPrice) ? 0 : double.Parse(AmountOfPrice);
            }
            catch (Exception ex)
            {
                return -1;
            }
            return int_AmountText * int_AmountOfPrice;
        }

        public List<string> GetMeasureList
        {
            get
            {
                List<string> result = new List<string>();

                switch (AppSettings.GetLanguage())
                {
                    case Constants.LanUz:
                        result = ((string[])Application.Current.Resources[Constants.MeasureList_uz]).ToList();
                        break;
                    case Constants.LanUzCyrl:
                        result = ((string[])Application.Current.Resources[Constants.MeasureList_uz_cyrl]).ToList();
                        break;
                    case Constants.LanEn:
                        result = ((string[])Application.Current.Resources[Constants.MeasureList_en]).ToList();
                        break;
                    case Constants.LanRu:
                        result = ((string[])Application.Current.Resources[Constants.MeasureList_ru]).ToList();
                        break;
                }

                return result;
            }
        }
    }
}
