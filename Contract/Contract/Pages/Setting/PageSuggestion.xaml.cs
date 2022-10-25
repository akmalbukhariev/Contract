using Contract.ViewModel.Pages.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contract.Pages.Setting
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageSuggestion : IPage
    { 
        public PageSuggestion()
        {
            InitializeComponent();

            SetModel(new PageSuggestionViewModel(Navigation));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            switch (AppSettings.GetLanguage())
            {
                case Constants.LanUz:
                    PModel.OfferList = ((string[])Application.Current.Resources[Constants.OfferList_uz]).ToList();
                    break;
                case Constants.LanUzCyrl:
                    PModel.OfferList = ((string[])Application.Current.Resources[Constants.OfferList_uz_cyrl]).ToList();
                    break;
                case Constants.LanEn:
                    PModel.OfferList = ((string[])Application.Current.Resources[Constants.OfferList_en]).ToList();
                    break;
                case Constants.LanRu:
                    PModel.OfferList = ((string[])Application.Current.Resources[Constants.OfferList_ru]).ToList();
                    break;
            }

            if (PModel.OfferList.Count > 0)
                PModel.SelectedOffer = PModel.OfferList[0];
        }

        PageSuggestionViewModel PModel
        {
            get
            {
                return Model as PageSuggestionViewModel;
            }
        } 
    }
}