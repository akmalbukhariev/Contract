using Contract.Model;
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
        public ObservableCollection<ChildMenuItem> MenuList { get => GetValue<ObservableCollection<ChildMenuItem>>(); set => SetValue(value); }
        public PageMainViewModel()
        {
            MenuList = new ObservableCollection<ChildMenuItem>();

            IDNumber = "1234567";
            TextCount1 = RSC.ApplicableContracts2;
            TextCount2 = RSC.UnconfirmedContracts2;
        }

        public void Init()
        {
            MenuList.Add(new ChildMenuItem() {ID = Constant.Menu1, Name = RSC.UnconfirmedContracts1 });
            MenuList.Add(new ChildMenuItem() {ID = Constant.Menu2, Name = RSC.ApplicableContracts1 });
            MenuList.Add(new ChildMenuItem() {ID = Constant.Menu3, Name = RSC.CanceledContracts });
        }
    }
}
