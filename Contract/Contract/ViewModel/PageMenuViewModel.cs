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
            MenuList = new ObservableCollection<Menu>();
        }

        public void InitMenu()
        {
            IDNumber = RSC.IDNumber + " \n 1234567";

            Menu menu1 = new Menu() 
            {
                ID = Constant.Menu1,
                MenuHeight = 205,
                HImage = "contracts",
                HText = RSC.AllContracts,
                HShowImage = "showMenu",
                ChildMenuList = new ObservableCollection<ChildMenuItem>()
                {
                    new ChildMenuItem(){ID = Constant.Menu1_1, Name = RSC.UnconfirmedContracts1},
                    new ChildMenuItem(){ID = Constant.Menu1_2, Name = RSC.ApplicableContracts1},
                    new ChildMenuItem(){ID = Constant.Menu1_3, Name = RSC.CanceledContracts},
                    new ChildMenuItem(){ID = Constant.Menu1_4, Name = RSC.CreateContract}
                }
            };

            Menu menu2 = new Menu()
            {
                ID = Constant.Menu2,
                HImage = "statistics",
                HText = RSC.ProfitAndLoss
            };

            Menu menu3 = new Menu()
            {
                ID = Constant.Menu3,
                HImage = "clients",
                HText = RSC.Customers
            };

            Menu menu4 = new Menu() 
            {
                ID = Constant.Menu4,
                MenuHeight = 190,
                HImage = "templates",
                HText = RSC.Templates,
                HShowImage = "showMenu",
                ChildMenuList = new ObservableCollection<ChildMenuItem>()
                {
                    new ChildMenuItem(){ID = Constant.Menu4_1, Name = RSC.ContractTemplates},
                    new ChildMenuItem(){ID = Constant.Menu4_2, Name = RSC.CreatingContractTemplate},
                    new ChildMenuItem(){ID = Constant.Menu4_3, Name = RSC.ContractNumberSequence},
                    new ChildMenuItem(){ID = Constant.Menu4_4, Name = RSC.CreateContractNumber}
                }
            };

            Menu menu5 = new Menu() 
            {
                ID = Constant.Menu5,
                MenuHeight = 160,
                HImage = "profile",
                HText = RSC.PersonalCabinet,
                HShowImage = "showMenu",
                ChildMenuList = new ObservableCollection<ChildMenuItem>()
                {
                    new ChildMenuItem(){ID = Constant.Menu5_1, Name = RSC.EditRequisiteInformation},
                    new ChildMenuItem(){ID = Constant.Menu5_2, Name = RSC.EditAccountPassword},
                    new ChildMenuItem(){ID = Constant.Menu5_3, Name = RSC.CompanyLogo}
                }
            };

            Menu menu6 = new Menu()
            {
                ID = Constant.Menu6,
                HImage = "settings",
                HText = RSC.Settings
            };

            Menu menu7 = new Menu()
            {
                ID = Constant.Menu7,
                HImage = "info",
                HText = RSC.AboutSoftware
            };

            Menu menu8 = new Menu()
            {
                ID = Constant.Menu8,
                HImage = "support",
                HText = RSC.ProposalsAndObjections
            };

            Menu menu9 = new Menu()
            {
                ID = Constant.Menu9,
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

        public void Clean()
        {
            MenuList.Clear();
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
