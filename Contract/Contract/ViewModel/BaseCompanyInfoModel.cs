using LibContract.HttpModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Contract.ViewModel
{
    public class BaseCompanyInfoModel : BaseModel
    {
        #region Properties
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
        public int PositionOfSignatory_index { get => GetValue<int>(); set => SetValue(value); }
        public string FullNameOfSignatory { get => GetValue<string>(); set => SetValue(value); }
        public bool IsAccountProvided { get => GetValue<bool>(); set => SetValue(value); }
        public string AccountantName { get => GetValue<string>(); set => SetValue(value); }
        public bool IsCounselProvided { get => GetValue<bool>(); set => SetValue(value); }
        public string CounselName { get => GetValue<string>(); set => SetValue(value); }
        public string LogoImageStr { get => GetValue<string>(); set => SetValue(value); }
        public ImageSource LogoImage { get => GetValue<ImageSource>(); set => SetValue(value); }
        public string CreatedDate { get => GetValue<string>(); set => SetValue(value); }

        public List<string> PositionList { get => GetValue<List<string>>(); set => SetValue(value); }
        #endregion

        public BaseCompanyInfoModel()
        {
            PositionList = new List<string>();
        }

        public BaseCompanyInfoModel(INavigation navigation) : base(navigation)
        {
            PositionList = new List<string>();
        }

        public BaseCompanyInfoModel Copy(BaseCompanyInfoModel other)
        {
            BaseCompanyInfoModel newModel = new BaseCompanyInfoModel();
            newModel.CompanyName = this.CompanyName;
            newModel.AddressOfCompany = this.AddressOfCompany;
            newModel.AccountNumber = this.AccountNumber;
            newModel.CompanyStir = this.CompanyStir;
            newModel.NameOfBank = newModel.NameOfBank;
            newModel.BankCode = this.BankCode;
            newModel.AreYouQQSPayer = this.AreYouQQSPayer;
            newModel.QQSCode = this.QQSCode;
            newModel.PhoneNnumberOfCompany = this.PhoneNnumberOfCompany;
            newModel.PositionOfSignatory = this.PositionOfSignatory;
            newModel.PositionOfSignatory_index = this.PositionOfSignatory_index;
            newModel.FullNameOfSignatory = this.FullNameOfSignatory;
            newModel.IsAccountProvided = this.IsAccountProvided;
            newModel.AccountantName = this.AccountantName;
            newModel.IsCounselProvided = this.IsCounselProvided;
            newModel.CounselName = this.CounselName;

            return newModel;
        }

        public bool Equals(BaseCompanyInfoModel other)
        {
            //bool rr = CompanyName.Equals(other.CompanyName);

            bool res = CompanyName.Equals(other.CompanyName) &&
                       AddressOfCompany.Equals(other.AddressOfCompany) &&
                       AccountNumber.Equals(other.AccountNumber) &&
                       CompanyStir.Equals(other.CompanyStir) &&
                       NameOfBank.Equals(other.NameOfBank) &&
                       BankCode.Equals(other.BankCode) &&
                       AreYouQQSPayer.Equals(other.AreYouQQSPayer) &&
                       QQSCode.Equals(other.QQSCode) &&
                       PhoneNnumberOfCompany.Equals(PhoneNnumberOfCompany) &&
                       PositionOfSignatory.Equals(other) &&
                       PositionOfSignatory_index.Equals(other.PositionOfSignatory_index) &&
                       FullNameOfSignatory.Equals(FullNameOfSignatory) &&
                       IsAccountProvided.Equals(other.IsAccountProvided) &&
                       AccountantName.Equals(other.AccountantName) &&
                       IsCounselProvided.Equals(other.IsCounselProvided) &&
                       CounselName.Equals(other.CounselName);

            return res;
        }
         
        public bool IsFieildEmpty()
        {
            bool res1 = string.IsNullOrEmpty(CompanyName) ||
                        string.IsNullOrEmpty(AddressOfCompany) ||
                        string.IsNullOrEmpty(AccountNumber) ||
                        string.IsNullOrEmpty(CompanyStir) ||
                        string.IsNullOrEmpty(NameOfBank) ||
                        string.IsNullOrEmpty(BankCode) ||
                        string.IsNullOrEmpty(PhoneNnumberOfCompany) ||
                        string.IsNullOrEmpty(PositionOfSignatory) ||
                        string.IsNullOrEmpty(FullNameOfSignatory);

            //bool res2 = AreYouQQSPayer;
            //bool res3 = IsAccountProvided;
            //bool res4 = IsCounselProvided;
            //
            //if (AreYouQQSPayer)
            //    res2 = string.IsNullOrEmpty(QQSCode?.Trim());
            //
            //if (IsAccountProvided)
            //    res3 = string.IsNullOrEmpty(AccountantName?.Trim());
            //
            //if (IsCounselProvided)
            //    res4 = string.IsNullOrEmpty(CounselName?.Trim());

            return res1;// || res2 || res3 || res4;
        }

        public CompanyInfo GetCompanyInfo()
        {
            return new CompanyInfo()
            {
                user_phone_number = ControlApp.UserInfo.phone_number,
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
                position_of_signer_index = PositionOfSignatory_index,
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
