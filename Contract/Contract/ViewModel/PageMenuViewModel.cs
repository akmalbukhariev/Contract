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
            IDNumber = RSC.IDNumber + $" \n {ControlApp.UserInfo.phone_number}";

            Menu menu1 = new Menu() 
            {
                ID = Constants.Menu1,
                MenuHeight = 205,
                HImage = "contracts",
                HText = RSC.AllContracts,
                HShowImage = "showMenu",
                ChildMenuList = new ObservableCollection<ChildMenuItem>()
                {
                    new ChildMenuItem(){ID = Constants.Menu1_1, Name = RSC.UnapprovedContracts1},
                    new ChildMenuItem(){ID = Constants.Menu1_2, Name = RSC.ApprovedContracts1},
                    new ChildMenuItem(){ID = Constants.Menu1_3, Name = RSC.CanceledContracts},
                    new ChildMenuItem(){ID = Constants.Menu1_4, Name = RSC.CreateContract}
                }
            };

            Menu menu2 = new Menu()
            {
                ID = Constants.Menu2,
                HImage = "statistics",
                HText = RSC.ProfitAndLoss
            };

            Menu menu3 = new Menu()
            {
                ID = Constants.Menu3,
                HImage = "clients",
                HText = RSC.Customers
            };

            Menu menu4 = new Menu() 
            {
                ID = Constants.Menu4,
                MenuHeight = 190,
                HImage = "templates",
                HText = RSC.Templates,
                HShowImage = "showMenu",
                ChildMenuList = new ObservableCollection<ChildMenuItem>()
                {
                    new ChildMenuItem(){ID = Constants.Menu4_1, Name = RSC.ContractTemplates},
                    new ChildMenuItem(){ID = Constants.Menu4_2, Name = RSC.CreatingContractTemplate},
                    new ChildMenuItem(){ID = Constants.Menu4_3, Name = RSC.ContractNumberSequence},
                    new ChildMenuItem(){ID = Constants.Menu4_4, Name = RSC.CreateContractNumber}
                }
            };

            Menu menu5 = new Menu() 
            {
                ID = Constants.Menu5,
                MenuHeight = 160,
                HImage = "profile",
                HText = RSC.PersonalCabinet,
                HShowImage = "showMenu",
                ChildMenuList = new ObservableCollection<ChildMenuItem>()
                {
                    new ChildMenuItem(){ID = Constants.Menu5_1, Name = RSC.EditRequisiteInformation},
                    new ChildMenuItem(){ID = Constants.Menu5_2, Name = RSC.EditAccountPassword},
                    new ChildMenuItem(){ID = Constants.Menu5_3, Name = RSC.CompanyLogo}
                }
            };

            Menu menu6 = new Menu()
            {
                ID = Constants.Menu6,
                HImage = "settings",
                HText = RSC.Settings
            };

            Menu menu7 = new Menu()
            {
                ID = Constants.Menu7,
                HImage = "info",
                HText = RSC.AboutSoftware
            };

            Menu menu8 = new Menu()
            {
                ID = Constants.Menu8,
                HImage = "support",
                HText = RSC.ProposalsAndObjections
            };

            Menu menu9 = new Menu()
            {
                ID = Constants.Menu9,
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
