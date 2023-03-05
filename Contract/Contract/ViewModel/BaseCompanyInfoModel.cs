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
        public int Id { get; set; }
        public string CompanyName { get => GetValue<string>(); set => SetValue(value); }
        public string SelectedDocument { get => GetValue<string>(); set => SetValue(value); }
        public int SelectedDocument_index { get => GetValue<int>(); set => SetValue(value); }
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

        public List<string> DocumentList { get => GetValue<List<string>>(); set => SetValue(value); }
        public List<string> PositionList { get => GetValue<List<string>>(); set => SetValue(value); }
        #endregion

        public BaseCompanyInfoModel()
        {
            Init();
        }

        public BaseCompanyInfoModel(INavigation navigation) : base(navigation)
        {
            Init();
        }

        public BaseCompanyInfoModel Copy()
        {
            BaseCompanyInfoModel newModel = new BaseCompanyInfoModel();
            newModel.Id = this.Id;
            newModel.CompanyName = this.CompanyName;
            newModel.SelectedDocument = this.SelectedDocument;
            newModel.SelectedDocument_index = this.SelectedDocument_index;
            newModel.AddressOfCompany = this.AddressOfCompany;
            newModel.AccountNumber = this.AccountNumber;
            newModel.CompanyStir = this.CompanyStir;
            newModel.NameOfBank = this.NameOfBank;
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

        private void Init()
        {
            CompanyName = "";
            SelectedDocument = "";
            AddressOfCompany = "";
            AccountNumber = "";
            AccountantName = "";
            CompanyStir = "";
            NameOfBank = "";
            BankCode = "";
            QQSCode = "";
            PhoneNnumberOfCompany = "";
            PositionOfSignatory = "";
            FullNameOfSignatory = "";
            AccountantName = "";
            CounselName = "";
            CreatedDate = "";

            PositionList = new List<string>();
        }

        public bool Equals(BaseCompanyInfoModel other)
        { 
            bool res = CompanyName.Equals(other.CompanyName) &&
                       SelectedDocument.Equals(other.SelectedDocument) &&
                       SelectedDocument_index.Equals(other.SelectedDocument_index) &&
                       AddressOfCompany.Equals(other.AddressOfCompany) &&
                       AccountNumber.Equals(other.AccountNumber) &&
                       CompanyStir.Equals(other.CompanyStir) &&
                       NameOfBank.Equals(other.NameOfBank) &&
                       BankCode.Equals(other.BankCode) &&
                       AreYouQQSPayer.Equals(other.AreYouQQSPayer) &&
                       QQSCode.Equals(other.QQSCode) &&
                       PhoneNnumberOfCompany.Equals(PhoneNnumberOfCompany) &&
                       PositionOfSignatory.Equals(other.PositionOfSignatory) && 
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
                        string.IsNullOrEmpty(SelectedDocument) ||
                        string.IsNullOrEmpty(AddressOfCompany) ||
                        string.IsNullOrEmpty(AccountNumber) ||
                        string.IsNullOrEmpty(CompanyStir) ||
                        string.IsNullOrEmpty(NameOfBank) ||
                        string.IsNullOrEmpty(BankCode) ||
                        string.IsNullOrEmpty(PhoneNnumberOfCompany) ||
                        string.IsNullOrEmpty(PositionOfSignatory) ||
                        string.IsNullOrEmpty(FullNameOfSignatory);

            bool res2 = AreYouQQSPayer;
            //bool res3 = IsAccountProvided;
            //bool res4 = IsCounselProvided;
            //
            if (AreYouQQSPayer)
                res2 = string.IsNullOrEmpty(QQSCode?.Trim());
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
                id = Id,
                user_phone_number = ControlApp.UserInfo.phone_number.RemoveWhitespace(),
                company_name = CompanyName.Trim(),
                document = SelectedDocument.Trim(),
                document_index = SelectedDocument_index,
                address_of_company = AddressOfCompany.Trim(),
                account_number = AccountNumber.RemoveWhitespace(),
                stir_of_company = CompanyStir.RemoveWhitespace(),
                name_of_bank = NameOfBank.Trim(),
                bank_code = BankCode.RemoveWhitespace(),
                are_you_qqs_payer = AreYouQQSPayer ? 1 : 0,
                qqs_number = QQSCode.RemoveWhitespace(),
                company_phone_number = PhoneNnumberOfCompany.RemoveWhitespace(),
                position_of_signer = PositionOfSignatory.Trim(),
                position_of_signer_index = PositionOfSignatory_index,
                name_of_signer = FullNameOfSignatory.Trim(),
                is_accountant_provided = IsAccountProvided ? 1 : 0,
                accountant_name = AccountantName.Trim(),
                is_legal_counsel_provided = IsCounselProvided ? 1 : 0,
                counsel_name = CounselName.Trim(),
                company_logo_url = LogoImageStr.Trim(),
                created_date = CreatedDate.Trim()
            };
        }

        public void SetCompanyInfo(CompanyInfo info)
        {
            Id = info.id;
            CompanyName = info.company_name;
            SelectedDocument = info.document;
            SelectedDocument_index = info.document_index;
            AddressOfCompany = info.address_of_company;
            AccountNumber = info.account_number;
            AccountantName = info.accountant_name;
            CompanyStir = info.stir_of_company;
            NameOfBank = info.name_of_bank;
            BankCode = info.bank_code;
            AreYouQQSPayer = info.are_you_qqs_payer == 1? true : false;
            QQSCode = info.qqs_number;
            PhoneNnumberOfCompany = info.company_phone_number;
            PositionOfSignatory = info.position_of_signer;
            PositionOfSignatory_index = info.position_of_signer_index;
            FullNameOfSignatory = info.name_of_signer;
            IsAccountProvided = info.is_accountant_provided == 1 ? true : false;
            AccountantName = info.accountant_name;
            IsCounselProvided = info.is_legal_counsel_provided == 1 ? true : false;
            CounselName = info.counsel_name;
            LogoImageStr = info.company_logo_url;
            CreatedDate = info.created_date;
        }
    }
}
