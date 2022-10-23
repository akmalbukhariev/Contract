using Contract.HttpResponse;
using Contract.Model;
using Contract.Net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Contract.ViewModel
{
    public class PageMainViewModel : BaseModel
    {
        public string IDNumber { get => GetValue<string>(); set => SetValue(value); }
        public string TextCount1 { get => GetValue<string>(); set => SetValue(value); }
        public string TextCount2 { get => GetValue<string>(); set => SetValue(value); }
        public string TextValue1 { get => GetValue<string>(); set => SetValue(value); }
        public string TextValue2 { get => GetValue<string>(); set => SetValue(value); }
        public ObservableCollection<ChildMenuItem> MenuList { get => GetValue<ObservableCollection<ChildMenuItem>>(); set => SetValue(value); }

        public PageMainViewModel()
        {
            MenuList = new ObservableCollection<ChildMenuItem>();

            IDNumber = "1234567"; 
        }

        public void Init()
        {
            TextCount1 = RSC.ApprovedContracts2;
            TextCount2 = RSC.UnapprovedContracts2;

            TextValue1 = "0";
            TextValue2 = "0";

            MenuList.Add(new ChildMenuItem() {ID = Constants.Menu1, Name = RSC.UnapprovedContracts1 });
            MenuList.Add(new ChildMenuItem() {ID = Constants.Menu2, Name = RSC.ApprovedContracts1 });
            MenuList.Add(new ChildMenuItem() {ID = Constants.Menu3, Name = RSC.CanceledContracts });
        }

        public async void RequestInfo()
        {
            if (!ControlApp.InternetOk()) return;

            HttpModels.ApprovedUnapprovedContract request = new HttpModels.ApprovedUnapprovedContract()
            {
                user_phone_number = ControlApp.UserInfo.phone_number,
                user_stir = "111122",
                use_is_approved = 0
            };

            ControlApp.ShowLoadingView(RSC.PleaseWait);
            ResponseApprovedUnapprovedContract response = await Net.HttpService.GetApprovedOrUnapprovedContract(request);
            ControlApp.CloseLoadingView();

            if (response.result)
            {
                TextValue1 = response.data != null ? response.data.Where(item => item.is_approved == 1).ToList().Count.ToString() : "0";
                TextValue2 = response.data != null ? response.data.Where(item => item.is_approved == 0).ToList().Count.ToString() : "0";
            }
        }

        public void Clean()
        {
            MenuList.Clear();
        }
    }
}
