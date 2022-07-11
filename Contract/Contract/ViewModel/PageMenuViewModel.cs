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
                HText = RSC.AllContracts,
                HShowImage = "showMenu",
                ChildMenuList = new ObservableCollection<ChildMenuItem>()
                {
                    new ChildMenuItem(){Name = RSC.UnconfirmedContracts1},
                    new ChildMenuItem(){Name = RSC.ApplicableContracts1},
                    new ChildMenuItem(){Name = RSC.CanceledContracts},
                    new ChildMenuItem(){Name = RSC.CreateContract}
                }
            };

            Menu menu2 = new Menu()
            {
                HImage = "statistics",
                HText = RSC.ProfitAndLoss
            };

            Menu menu3 = new Menu()
            {
                HImage = "clients",
                HText = RSC.Customers
            };

            Menu menu4 = new Menu() 
            {
                MenuHeight = 190,
                HImage = "templates",
                HText = RSC.Templates,
                HShowImage = "showMenu",
                ChildMenuList = new ObservableCollection<ChildMenuItem>()
                {
                    new ChildMenuItem(){Name = RSC.ContractTemplates},
                    new ChildMenuItem(){Name = RSC.CreatingContractTemplate},
                    new ChildMenuItem(){Name = RSC.ContractNumberSequence},
                    new ChildMenuItem(){Name = RSC.CreateContractNumber}
                }
            };

            Menu menu5 = new Menu() 
            {
                MenuHeight = 160,
                HImage = "profile",
                HText = RSC.PersonalCabinet,
                HShowImage = "showMenu",
                ChildMenuList = new ObservableCollection<ChildMenuItem>()
                {
                    new ChildMenuItem(){Name = RSC.EditRequisiteInformation},
                    new ChildMenuItem(){Name = RSC.EditAccountPassword},
                    new ChildMenuItem(){Name = RSC.CompanyLogo}
                }
            };

            Menu menu6 = new Menu()
            {
                HImage = "settings",
                HText = RSC.Settings
            };

            Menu menu7 = new Menu()
            {
                HImage = "info",
                HText = RSC.AboutSoftware
            };

            Menu menu8 = new Menu()
            {
                HImage = "support",
                HText = RSC.ProposalsAndObjections
            };

            Menu menu9 = new Menu()
            {
                HImage = "exit",
                HText = RSC.SignOut
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
