using Contract.Model;
using Contract.Net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            TextCount1 = RSC.ApplicableContracts2;
            TextCount2 = RSC.UnapprovedContracts2;

            TextValue1 = "0";
            TextValue2 = "0";

            MenuList.Add(new ChildMenuItem() {ID = Constant.Menu1, Name = RSC.UnapprovedContracts1 });
            MenuList.Add(new ChildMenuItem() {ID = Constant.Menu2, Name = RSC.ApplicableContracts1 });
            MenuList.Add(new ChildMenuItem() {ID = Constant.Menu3, Name = RSC.CanceledContracts });
        }

        public async void RequestInfo()
        {
            if (!ControlApp.InternetOk()) return;

            ControlApp.ShowLoadingView(RSC.PleaseWait);
            ResponseApplicableContract response1 = await HttpService.GetApplicableContract(ControlApp.UserInfo.phone_number);
            ResponseUnapprovedContract response2 = await HttpService.GetUnapprovedContract(ControlApp.UserInfo.phone_number);
            ControlApp.CloseLoadingView();

            if (response1.result)
            {
                TextValue1 = response1.data != null ? response1.data.Count.ToString() : "0";
            }

            if (response2.result)
            {
                TextValue2 = response2.data != null ? response2.data.Count.ToString() : "0";
            }
        }

        public void Clean()
        {
            MenuList.Clear();
        }
    }
}
