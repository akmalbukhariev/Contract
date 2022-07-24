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
        public bool IsVisible { get => GetValue<bool>(); set => SetValue(value); }
        public int MenuHeight { get => GetValue<int>(); set => SetValue(value); }
        public string HImage { get => GetValue<string>(); set => SetValue(value); }
        public string HText { get => GetValue<string>(); set => SetValue(value); }
        public string HShowImage { get => GetValue<string>(); set => SetValue(value); }

        public ChildMenuItem SelectedChildItem { get => GetValue<ChildMenuItem>(); set => SetValue(value); }

        public ObservableCollection<ChildMenuItem> ChildMenuList { get => GetValue<ObservableCollection<ChildMenuItem>>(); set => SetValue(value); }

        public Menu()
        {
            IsVisible = false;
            MenuHeight = 0;
        }
    }
}
