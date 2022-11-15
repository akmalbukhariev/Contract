using Contract.ViewModel.Pages.ContractNumber;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contract.Pages.ContractNumber
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageEditContractNumber : IPage
    {
        public PageEditContractNumber(bool isReadonly = false)
        {
            InitializeComponent();

            navBar.IsThisModalPage = isReadonly;
            entyFormat2.Entry.IsReadOnly = isReadonly;
            entyFormat3.Entry.IsReadOnly = isReadonly;
            btnSave.IsVisible = !isReadonly;

            SetModel(new PageEditContractNumberViewModel(Navigation));

            rbFormat1.Content = RSC.Format + " 1";
            rbFormat2.Content = RSC.Format + " 2";
            rbFormat3.Content = RSC.Format + " 3";

            entyFormat2.Entry.TextChanged += Format_TextChanged;
            entyFormat3.Entry.TextChanged += Format_TextChanged;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            PModel.RequestInfo();
        }

        private void Format_TextChanged(object sender, TextChangedEventArgs e)
        {
            string strSequenceNumber_1 = PModel.SequenceNumber1.Replace("-", "").Trim();
            string strSequenceNumber_2 = PModel.SequenceNumber2.Replace("-", "").Trim();
            string strSequenceNumber_3 = PModel.SequenceNumber3.Replace("-", "").Trim();

            if (sender == entyFormat2.Entry && PModel.CheckFormat_2)
            {
                PModel.YourContractNumber = $"{PModel.Option}-{strSequenceNumber_2}"; 
            }
            else if (sender == entyFormat3.Entry && PModel.CheckFormat_3)
            {
                PModel.YourContractNumber = $"{strSequenceNumber_3}-{PModel.Option}"; 
            } 
        }

        void OnRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            string strSequenceNumber_1 = PModel.SequenceNumber1.Replace("-", "").Trim();
            string strSequenceNumber_2 = PModel.SequenceNumber2.Replace("-", "").Trim();
            string strSequenceNumber_3 = PModel.SequenceNumber3.Replace("-", "").Trim();
                      
            if (sender == rbFormat1)
            {
                rbFormat1.TextColor = Color.White;
                rbFormat2.TextColor = Color.Black;
                rbFormat3.TextColor = Color.Black;

                lbFormat1_1.TextColor = Color.White; 
                lbFormat2_1.TextColor = Color.Gray; 
                lbFormat3_1.TextColor = Color.Gray;

                PModel.EnableFomat_1 = true;
                entyFormat2.Entry.IsEnabled = PModel.EnableFomat_2 = false;
                entyFormat3.Entry.IsEnabled =  PModel.EnableFomat_3 = false;
                PModel.YourContractNumber = $"{strSequenceNumber_1}";
            }
            else if (sender == rbFormat2)
            { 
                rbFormat1.TextColor = Color.Black;
                rbFormat2.TextColor = Color.White;
                rbFormat3.TextColor = Color.Black;
              
                lbFormat1_1.TextColor = Color.Gray; 
                lbFormat2_1.TextColor = Color.White; 
                lbFormat3_1.TextColor = Color.Gray;

                PModel.EnableFomat_1 = false;
                entyFormat2.Entry.IsEnabled = PModel.EnableFomat_2 = true;
                entyFormat3.Entry.IsEnabled = PModel.EnableFomat_3 = false;
                PModel.YourContractNumber = $"{PModel.Option}-{strSequenceNumber_2}";
            }
            else if (sender == rbFormat3)
            {
                rbFormat1.TextColor = Color.Black;
                rbFormat2.TextColor = Color.Black;
                rbFormat3.TextColor = Color.White;

                lbFormat1_1.TextColor = Color.Gray;
                lbFormat2_1.TextColor = Color.Gray;
                lbFormat3_1.TextColor = Color.White;

                PModel.EnableFomat_1 = false;
                entyFormat2.Entry.IsEnabled = PModel.EnableFomat_2 = false;
                entyFormat3.Entry.IsEnabled = PModel.EnableFomat_3 = true;
                PModel.YourContractNumber = $"{strSequenceNumber_3}-{PModel.Option}";
            }
        }

        PageEditContractNumberViewModel PModel
        {
            get
            {
                return Model as PageEditContractNumberViewModel;
            }
        }

        private void Label_Tapped(object sender, EventArgs e)
        {
            if (sender == lbFormat1_1)
            {
                PModel.CheckFormat_1 = true;
            }
            else if (sender == lbFormat2_1 || sender == entyFormat2)
            {
                PModel.CheckFormat_2 = true;
            }
            else if (sender == lbFormat3_1 || sender == entyFormat3)
            {
                PModel.CheckFormat_3 = true;
            }
        }
    }
}