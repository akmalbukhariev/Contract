using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
  
namespace Contract
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            this.Items.Add(new Pages.Introduction.PageIntroduction());
             
            Routing.RegisterRoute(nameof(Pages.CanceledContracts.PageCanceledTable), typeof(Pages.CanceledContracts.PageCanceledTable));

            Routing.RegisterRoute(nameof(Pages.ChangePassword.PageChangePassword), typeof(Pages.ChangePassword.PageChangePassword));
            
            Routing.RegisterRoute(nameof(Pages.ContractNumber.PageChangeContractNumber), typeof(Pages.ContractNumber.PageChangeContractNumber));
            Routing.RegisterRoute(nameof(Pages.ContractNumber.PageContractNumberList), typeof(Pages.ContractNumber.PageContractNumberList));
            
            Routing.RegisterRoute(nameof(Pages.CreateContract.PageCreateContract1), typeof(Pages.CreateContract.PageCreateContract1));
            Routing.RegisterRoute(nameof(Pages.CreateContract.PageCreateContract2), typeof(Pages.CreateContract.PageCreateContract2));
            Routing.RegisterRoute(nameof(Pages.CreateContract.PageCreateContract3), typeof(Pages.CreateContract.PageCreateContract3)); 
            
            Routing.RegisterRoute(nameof(Pages.CurrentContracts.PageCurrentCancelContract), typeof(Pages.CurrentContracts.PageCurrentCancelContract));
            Routing.RegisterRoute(nameof(Pages.CurrentContracts.PageCurrentContractTable), typeof(Pages.CurrentContracts.PageCurrentContractTable));
            
            Routing.RegisterRoute(nameof(Pages.Customers.PageAddOrEditCustomer), typeof(Pages.Customers.PageAddOrEditCustomer));
            Routing.RegisterRoute(nameof(Pages.Customers.PageCustomerList), typeof(Pages.Customers.PageCustomerList));
            
            Routing.RegisterRoute(nameof(Pages.EditContract.PageEditPersonalContract), typeof(Pages.EditContract.PageEditPersonalContract));

            Routing.RegisterRoute(nameof(Pages.Introduction.PageIntroduction), typeof(Pages.Introduction.PageIntroduction));
            Routing.RegisterRoute(nameof(Pages.Introduction.PageLoginInfo), typeof(Pages.Introduction.PageLoginInfo));

            Routing.RegisterRoute(nameof(Pages.Login.PageLogin), typeof(Pages.Login.PageLogin));
            
            Routing.RegisterRoute(nameof(Pages.Setting.PageAbout), typeof(Pages.Setting.PageAbout));
            Routing.RegisterRoute(nameof(Pages.Setting.PageLanguage), typeof(Pages.Setting.PageSetting));
            Routing.RegisterRoute(nameof(Pages.Setting.PageSuggestion), typeof(Pages.Setting.PageSuggestion));
            
            Routing.RegisterRoute(nameof(Pages.SignUp.PageCheckID), typeof(Pages.SignUp.PageCheckID));
            Routing.RegisterRoute(nameof(Pages.SignUp.PageFillInfo), typeof(Pages.SignUp.PageFillInfo));
            Routing.RegisterRoute(nameof(Pages.SignUp.PageFinished), typeof(Pages.SignUp.PageFinished));
            Routing.RegisterRoute(nameof(Pages.SignUp.PageNewPassword), typeof(Pages.SignUp.PageNewPassword));
            Routing.RegisterRoute(nameof(Pages.SignUp.PagePhoneNumber), typeof(Pages.SignUp.PagePhoneNumber));
            Routing.RegisterRoute(nameof(Pages.SignUp.PageSkippedTime), typeof(Pages.SignUp.PageSkippedTime));
            Routing.RegisterRoute(nameof(Pages.SignUp.PageSpecialCode), typeof(Pages.SignUp.PageSpecialCode));

            Routing.RegisterRoute(nameof(Pages.TemplateContract.PageContractTemplate), typeof(Pages.TemplateContract.PageContractTemplate));
            Routing.RegisterRoute(nameof(Pages.TemplateContract.PageEditTemplateContract), typeof(Pages.TemplateContract.PageEditTemplateContract));
            Routing.RegisterRoute(nameof(Pages.TemplateContract.PageFullDescription), typeof(Pages.TemplateContract.PageFullDescription));

            Routing.RegisterRoute(nameof(Pages.UnapprovedContracts.PageUnapprovedCancelContract), typeof(Pages.UnapprovedContracts.PageUnapprovedCancelContract));
            Routing.RegisterRoute(nameof(Pages.UnapprovedContracts.PageContractSpecialCode), typeof(Pages.UnapprovedContracts.PageContractSpecialCode));
            Routing.RegisterRoute(nameof(Pages.UnapprovedContracts.PageERIKey), typeof(Pages.UnapprovedContracts.PageERIKey));
            Routing.RegisterRoute(nameof(Pages.UnapprovedContracts.PageTable), typeof(Pages.UnapprovedContracts.PageTable));

            Routing.RegisterRoute(nameof(Pages.PageM), typeof(Pages.PageM));
            Routing.RegisterRoute(nameof(Pages.PageMain), typeof(Pages.PageMain));
            Routing.RegisterRoute(nameof(Pages.PageMasterDetail), typeof(Pages.PageMasterDetail));
            Routing.RegisterRoute(nameof(Pages.PageMenu), typeof(Pages.PageMenu));
            Routing.RegisterRoute(nameof(Pages.PageMessage), typeof(Pages.PageMessage));

        }
    }
}