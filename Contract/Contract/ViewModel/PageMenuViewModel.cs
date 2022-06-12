using Contract.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Contract.ViewModel
{
    public class PageMenuViewModel : BaseModel
    {
        public string CompanyName { get => GetValue<string>(); set => SetValue(value); }
        public string IDNumber { get => GetValue<string>(); set => SetValue(value); }
         
        public ObservableCollection<Menu> MenuList { get => GetValue<ObservableCollection<Menu>>(); set => SetValue(value); }

        public PageMenuViewModel()
        {
            CompanyName = "\"Korxona nomi\" MCHJ";
            IDNumber = "ID Raqamingiz \n 1234567";
            MenuList = new ObservableCollection<Menu>();
        }

        public void InitMenu()
        {
            Menu menu1 = new Menu() 
            {
                MenuHeight = 205,
                HImage = "contracts",
                HText = "Shartnomalar",
                HShowImage = "showMenu",
                ChildMenuList = new ObservableCollection<ChildMenuItem>()
                {
                    new ChildMenuItem(){Name = "Tastiqlanmagan shartnomalar"},
                    new ChildMenuItem(){Name = "Amaldagi shartnomalar"},
                    new ChildMenuItem(){Name = "Bekor qilingan shartnomalar"},
                    new ChildMenuItem(){Name = "Shartnoma tuzish"}
                }
            };

            Menu menu2 = new Menu()
            {
                HImage = "statistics",
                HText = "Foyda va zarar"
            };

            Menu menu3 = new Menu()
            {
                HImage = "clients",
                HText = "Mijozlar"
            };

            Menu menu4 = new Menu() 
            {
                MenuHeight = 190,
                HImage = "templates",
                HText = "Shablonlar",
                HShowImage = "showMenu",
                ChildMenuList = new ObservableCollection<ChildMenuItem>()
                {
                    new ChildMenuItem(){Name = "Shartnoma shablonlari"},
                    new ChildMenuItem(){Name = "Shartnoma shablonini yaratish"},
                    new ChildMenuItem(){Name = "Shartnoma raqam ketma-ketligi"},
                    new ChildMenuItem(){Name = "Shartnoma raqamini yaratish"}
                }
            };

            Menu menu5 = new Menu() 
            {
                MenuHeight = 160,
                HImage = "profile",
                HText = "Shaxsiy kabinet",
                HShowImage = "showMenu",
                ChildMenuList = new ObservableCollection<ChildMenuItem>()
                {
                    new ChildMenuItem(){Name = "Rekvizit ma'lumotlarini tahrirlash"},
                    new ChildMenuItem(){Name = "Akkaunt parolini tahrirlash"},
                    new ChildMenuItem(){Name = "Korxona logotipi"}
                }
            };

            Menu menu6 = new Menu()
            {
                HImage = "settings",
                HText = "Sozlamalar"
            };

            Menu menu7 = new Menu()
            {
                HImage = "info",
                HText = "Dasturiy ta'minot haqida"
            };

            Menu menu8 = new Menu()
            {
                HImage = "support",
                HText = "Taklif va e'tirozlar"
            };

            Menu menu9 = new Menu()
            {
                HImage = "exit",
                HText = "Tizimdan chiqish"
            };

            MenuList.Add(menu1);
            MenuList.Add(menu2);
            MenuList.Add(menu3);
            MenuList.Add(menu4);
            MenuList.Add(menu5);
            MenuList.Add(menu6);
            MenuList.Add(menu7);
            MenuList.Add(menu8);
            MenuList.Add(menu9);
        }

        internal void HodeOrShowMenu(Menu item)
        {
            item.IsVisible = !item.IsVisible;
            item.HShowImage = item.HShowImage == "hideMenu"? "showMenu" : "hideMenu";

            var index = MenuList.IndexOf(item);
            MenuList.Remove(item);
            MenuList.Insert(index, item);
        }
    }
}
