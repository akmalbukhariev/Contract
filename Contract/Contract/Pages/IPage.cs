using Contract.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Contract.Pages
{ 
    public abstract class IPage : ContentPage
    {
        public delegate void NavigatePage(Page page);
        public event NavigatePage EventNavigatePage;

        public delegate void ShowMenu(bool show);
        public event ShowMenu EventShowMenu;

        public Control.ControlApp ControlApp => Control.ControlApp.Instance;

        protected BaseModel Model;

        public IPage()
        {
            BackgroundColor = Color.White; 

            NavigationPage.SetHasBackButton(this, false);
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected void SetModel(BaseModel model)
        {
            Model = model;
            BindingContext = Model;
        }
         
        protected async void ClickAnimationView<T>(T view) where T : View
        {
            await view.ScaleTo(0.8, 200);
            await view.ScaleTo(1, 200, Easing.SpringOut);
        }

        protected async void ChangeClickBackColor(Label label, Color changeColor, Color orgColor)
        {
            label.TextColor = changeColor;
            await Task.Delay(100);

            label.TextColor = orgColor;
            await Task.Delay(200);
        }

        protected virtual void OnNavigatePage(Page page = null)
        {
            EventNavigatePage?.Invoke(page);
        }

        protected virtual void OnShowMenu(bool show)
        {
            EventShowMenu?.Invoke(show);
        }

        protected string GetYesNoIcon(bool yes)
        {
            string strLan = string.Empty;
            switch (AppSettings.GetLanguage())
            {
                case Constants.LanUz:
                    strLan = yes ? "uz_Yes" : "uz_No";
                    break;
                case Constants.LanUzCyrl:
                    strLan = yes ? "uzK_Yes" : "uzK_No";
                    break;
                case Constants.LanEn:
                    strLan = yes ? "en_Yes" : "en_No";
                    break;
                case Constants.LanRu:
                    strLan = yes ? "ru_Yes" : "ru_No";
                    break;
            }

            return strLan;
        }
         
        protected List<string> GetCurrentList
        {
            get
            {
                List<string> result = new List<string>();

                switch (AppSettings.GetLanguage())
                {
                    case Constants.LanUz:
                        result = ((string[])Application.Current.Resources[Constants.CurrencyList_uz]).ToList();
                        break;
                    case Constants.LanUzCyrl:
                        result = ((string[])Application.Current.Resources[Constants.CurrencyList_uz_cyrl]).ToList();
                        break;
                    case Constants.LanEn:
                        result = ((string[])Application.Current.Resources[Constants.CurrencyList_en]).ToList();
                        break;
                    case Constants.LanRu:
                        result = ((string[])Application.Current.Resources[Constants.CurrencyList_ru]).ToList();
                        break;
                }

                return result;
            }
        }

        protected List<string> GetQQSList
        {
            get
            { 
                return ((string[])Application.Current.Resources[Constants.QqsList]).ToList();
            }
        }

        protected List<string> GetMeasureList
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

        protected List<string> GetPositionList
        {
            get
            {
                List<string> result = new List<string>();

                switch (AppSettings.GetLanguage())
                {
                    case Constants.LanUz:
                        result = ((string[])Application.Current.Resources[Constants.PositionList_uz]).ToList();
                        break;
                    case Constants.LanUzCyrl:
                        result = ((string[])Application.Current.Resources[Constants.PositionList_uz_cyrl]).ToList();
                        break;
                    case Constants.LanEn:
                        result = ((string[])Application.Current.Resources[Constants.PositionList_en]).ToList();
                        break;
                    case Constants.LanRu:
                        result = ((string[])Application.Current.Resources[Constants.PositionList_ru]).ToList();
                        break;
                }

                return result;
            }
        }
    }
}