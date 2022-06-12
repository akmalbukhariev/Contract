using Contract.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Contract.ViewModel
{
    public class PageMainViewModel : BaseModel
    {
        public string TextCount1 { get => GetValue<string>(); set => SetValue(value); }
        public string TextCount2 { get => GetValue<string>(); set => SetValue(value); }
        public ObservableCollection<ChildMenuItem> MenuList { get => GetValue<ObservableCollection<ChildMenuItem>>(); set => SetValue(value); }
        public PageMainViewModel()
        {
            MenuList = new ObservableCollection<ChildMenuItem>();
            TextCount1 = "Amaldagi \n shartnomalar";
            TextCount2 = "Tasdiqlanmagan \n shartnomalar";
        }

        public void Init()
        {
            MenuList.Add(new ChildMenuItem() { Name = "Tasdiqlanmagan shartnomalar" });
            MenuList.Add(new ChildMenuItem() { Name = "Amaldagi shartnomalar" });
            MenuList.Add(new ChildMenuItem() { Name = "Bekor qilingan shartnomalar" });
        }
    }
}
