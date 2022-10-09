using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Contract.ViewModel
{
    public class BaseCompanyInfoModel : BaseModel
    {
        public string CompanyName { get => GetValue<string>(); set => SetValue(value); }
        public string AddressOfCompany { get => GetValue<string>(); set => SetValue(value); }
        public string AccountNumber { get => GetValue<string>(); set => SetValue(value); }
        public string CompanyStir { get => GetValue<string>(); set => SetValue(value); }
        public string NameOfBank { get => GetValue<string>(); set => SetValue(value); }
        public string BankCode { get => GetValue<string>(); set => SetValue(value); }
        public bool AreYouQQSPayer { get => GetValue<bool>(); set => SetValue(value); }
        public string QQSCode { get => GetValue<string>(); set => SetValue(value); }
        public string PhoneNnumberOfCompany { get => GetValue<string>(); set => SetValue(value); }
        public string PositionOfSignatory { get => GetValue<string>(); set => SetValue(value); }
        public string FullNameOfSignatory { get => GetValue<string>(); set => SetValue(value); }
        public bool IsAccountProvided { get => GetValue<bool>(); set => SetValue(value); }
        public string AccountantName { get => GetValue<string>(); set => SetValue(value); }
        public bool IsCounselProvided { get => GetValue<bool>(); set => SetValue(value); }
        public string CounselName { get => GetValue<string>(); set => SetValue(value); }
        public string LogoImageStr { get => GetValue<string>(); set => SetValue(value); }
        public ImageSource LogoImage { get => GetValue<ImageSource>(); set => SetValue(value); }
        public string CreatedDate { get => GetValue<string>(); set => SetValue(value); }

        public List<string> PositionList { get => GetValue<List<string>>(); set => SetValue(value); }

        public BaseCompanyInfoModel()
        {
            PositionList = new List<string>();
        }

        public BaseCompanyInfoModel(INavigation navigation) : base(navigation)
        {
            PositionList = new List<string>();
        }

        public Net.CompanyInfo GetCompanyInfo()
        {
            return new Net.CompanyInfo()
            {
                user_phone_number = "12",
                company_name = CompanyName,
                address_of_company = AddressOfCompany,
                account_number = AccountNumber,
                stir_of_company = CompanyStir,
                name_of_bank = NameOfBank,
                bank_code = BankCode,
                are_you_qqs_payer = AreYouQQSPayer ? 1 : 0,
                qqs_number = QQSCode,
                company_phone_number = PhoneNnumberOfCompany,
                position_of_signer = PositionOfSignatory,
                name_of_signer = FullNameOfSignatory,
                is_accountant_provided = IsAccountProvided ? 1 : 0,
                accountant_name = AccountantName,
                is_legal_counsel_provided = IsCounselProvided ? 1 : 0,
                counsel_name = CounselName,
                company_logo_url = LogoImageStr,
                created_date = CreatedDate
            };
        }
    }
}
