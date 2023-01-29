using Contract.Model;
using LibContract.HttpModels;
using LibContract.HttpResponse;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contract.ViewModel.Pages.Setting
{
    public class PageDefaultContractListViewModel : BaseModel
    {
        public DefaultContract SelectedItem { get => GetValue<DefaultContract>(); set => SetValue(value); }
        public ObservableCollection<DefaultContract> DataList { get; set; }
        public PageDefaultContractListViewModel(INavigation navigation) : base(navigation)
        {
            DataList = new ObservableCollection<DefaultContract>();
        }

        public ICommand CommandShowPdfViewer => new Command(ShowPdfViewer);
        private void ShowPdfViewer()
        {
            ControlApp.Vibrate();
        }

        public async void RequestInfo()
        {
            DataList.Clear();
            ControlApp.ShowLoadingView(RSC.PleaseWait);
            ResponseReadyTemplate response = await Net.HttpService.GetAllReadyTemplateUrl();
            ControlApp.CloseLoadingView();

            if (!ControlApp.CheckResponse(response)) return;

            if (response.result)
            {
                DefaultContract temp = null;
                foreach (ReadyTemplate item in response.data)
                {    
                    string strName = "";
                    switch (AppSettings.GetLanguage())
                    {
                        case Constants.LanUz: strName = item.template_name_uz; break;
                        case Constants.LanEn: strName = item.template_name_en; break;
                        case Constants.LanRu: strName = item.template_name_ru; break;
                        case Constants.LanUzCyrl: strName = item.template_name_uz_cyrl; break;
                    }

                    DefaultContract newItem = new DefaultContract()
                    {
                        id = item.id,
                        Url = item.file_url,
                        Name = strName,
                    };
                    newItem.Selected = false;

                    //if (item.id == ControlApp.UserInfo.default_template_id)
                    //{
                    //    temp = new DefaultContract(newItem);
                    //    newItem.Selected = true;
                    //}

                    DataList.Add(newItem); 
                }
            } 
        }
    }
}
