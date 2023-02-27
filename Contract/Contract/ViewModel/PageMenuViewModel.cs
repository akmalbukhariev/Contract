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
            string strName = ControlApp.UserCompanyInfo != null ? ControlApp.UserCompanyInfo.company_name : "";
            CompanyName = $"{RSC.CompanyName}: {strName}";
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
                HText1 = RSC.UnapprovedContracts1,
                HText2 = RSC.ApprovedContracts1,
                HText3 = RSC.CanceledContracts,
                HText4 = RSC.CreateContract,
                ShowExpander = true
            };

            Menu menu2 = new Menu()
            {
                ID = Constants.Menu2,
                HImage = "statistics",
                HText = RSC.ProfitAndLoss,
                ShowStack = true
            };

            Menu menu3 = new Menu()
            {
                ID = Constants.Menu3,
                HImage = "clients",
                HText = RSC.Customers,
                ShowStack = true
            };

            Menu menu4 = new Menu() 
            {
                ID = Constants.Menu4,
                MenuHeight = 190,
                HImage = "templates",
                HText = RSC.Templates,
                HShowImage = "showMenu",
                HText1 = RSC.ContractTemplates,
                HText2 = RSC.CreatingContractTemplate,
                HText3 = RSC.ContractNumberSequence,
                HText4 = RSC.CreateContractNumber,
                ShowExpander = true 
            };

            Menu menu5 = new Menu() 
            {
                ID = Constants.Menu5,
                MenuHeight = 160,
                HImage = "profile",
                HText = RSC.PersonalCabinet,
                HShowImage = "showMenu",
                HText1 = RSC.EditRequisiteInformation,
                HText2 = RSC.EditAccountPassword,
                HText3 = RSC.CompanyLogo,
                ShowExpander = true,
                ShowGrid1 = false,
                ShowGrid2 = true 
            };

            Menu menu6 = new Menu()
            {
                ID = Constants.Menu6,
                HImage = "settings",
                HText = RSC.Settings,
                ShowStack = true
            };

            Menu menu7 = new Menu()
            {
                ID = Constants.Menu7,
                HImage = "info",
                HText = RSC.AboutSoftware,
                ShowStack = true
            };

            Menu menu8 = new Menu()
            {
                ID = Constants.Menu8,
                HImage = "support",
                HText = RSC.ProposalsAndObjections,
                ShowStack = true
            };

            Menu menu9 = new Menu()
            {
                ID = Constants.Menu9,
                HImage = "exit",
                HText = RSC.SignOut,
                ShowStack = true
            };

            MenuList.Add(menu1);
            //MenuList.Add(menu2);
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
    }
}
