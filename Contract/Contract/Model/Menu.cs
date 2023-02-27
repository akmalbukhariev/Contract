using Contract.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Contract.Model
{
    public class ChildMenuItem : BaseModel
    {
        public string ID { get => GetValue<string>(); set => SetValue(value); }
        public string Name { get => GetValue<string>(); set => SetValue(value); }
    }
    public class Menu :  BaseModel
    {
        public string ID { get => GetValue<string>(); set => SetValue(value); }
        public bool ShowExpander { get => GetValue<bool>(); set => SetValue(value); }
        public bool ShowStack { get => GetValue<bool>(); set => SetValue(value); }
        public bool ShowGrid1 { get => GetValue<bool>(); set => SetValue(value); }
        public bool ShowGrid2 { get => GetValue<bool>(); set => SetValue(value); }
        public int MenuHeight { get => GetValue<int>(); set => SetValue(value); }
        public string HImage { get => GetValue<string>(); set => SetValue(value); }
        public string HText { get => GetValue<string>(); set => SetValue(value); }
        public string HShowImage { get => GetValue<string>(); set => SetValue(value); }
        public string HText1 { get => GetValue<string>(); set => SetValue(value); }
        public string HText2 { get => GetValue<string>(); set => SetValue(value); }
        public string HText3 { get => GetValue<string>(); set => SetValue(value); }
        public string HText4 { get => GetValue<string>(); set => SetValue(value); }
        public ChildMenuItem SelectedChildItem { get => GetValue<ChildMenuItem>(); set => SetValue(value); }

        public ObservableCollection<ChildMenuItem> ChildMenuList { get => GetValue<ObservableCollection<ChildMenuItem>>(); set => SetValue(value); }

        public Menu()
        {
            ShowExpander = false;
            ShowStack = false;
            ShowGrid1 = true;
            ShowGrid2 = false;
            MenuHeight = 0;
        }
    }
}
