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
        public PageEditContractNumber()
        {
            InitializeComponent();

            SetModel(new PageEditContractNumberViewModel(Navigation));

            entyFormat1.Entry.TextChanged += Format_TextChanged;
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
            string strYearFormat_1 = PModel.YearFormat_1.Replace("-", "");
            string strYearFormat_2 = PModel.YearFormat_2.Replace("-", "");
            string strYearFormat_3 = PModel.YearFormat_3.Replace("-", "");

            string strTimeFormat_1 = PModel.TimeFormat_1.Replace("-", "");
            string strTimeFormat_2 = PModel.TimeFormat_2.Replace("-", "");
            string strTimeFormat_3 = PModel.TimeFormat_3.Replace("-", "");

            if (sender == entyFormat1.Entry && PModel.CheckFormat_1)
            {
                PModel.YourContractNumber = $"{strYearFormat_1} - {PModel.Format_1} - {strTimeFormat_1}";
                PModel.Format_2 = PModel.Format_1;
                PModel.Format_3 = PModel.Format_1;
            }
            else if (sender == entyFormat2.Entry && PModel.CheckFormat_2)
            {
                PModel.YourContractNumber = $"{PModel.Format_2} - {strYearFormat_2} - {strTimeFormat_2}";
                PModel.Format_1 = PModel.Format_2;
                PModel.Format_3 = PModel.Format_2;
            }
            else if (sender == entyFormat3.Entry && PModel.CheckFormat_3)
            {
                PModel.YourContractNumber = $"{strYearFormat_3} - {strTimeFormat_3} - {PModel.Format_3}";
                PModel.Format_1 = PModel.Format_3;
                PModel.Format_2 = PModel.Format_3;
            } 
        }

        void OnRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            string strYearFormat_1 = PModel.YearFormat_1.Replace("-", "");
            string strYearFormat_2 = PModel.YearFormat_2.Replace("-", "");
            string strYearFormat_3 = PModel.YearFormat_3.Replace("-", "");
                    
            string strTimeFormat_1 = PModel.TimeFormat_1.Replace("-", "");
            string strTimeFormat_2 = PModel.TimeFormat_2.Replace("-", "");
            string strTimeFormat_3 = PModel.TimeFormat_3.Replace("-", "");

            if (sender == rbFormat1)
            {
                rbFormat1.TextColor = Color.White;
                rbFormat2.TextColor = Color.Black;
                rbFormat3.TextColor = Color.Black;

                lbFormat1_1.TextColor = Color.White;
                lbFormat1_2.TextColor = Color.White;
                lbFormat2_1.TextColor = Color.Gray;
                lbFormat2_2.TextColor = Color.Gray;
                lbFormat3_1.TextColor = Color.Gray;
                lbFormat3_2.TextColor = Color.Gray;

                entyFormat1.Entry.IsEnabled = PModel.EnableFomat_1 = true;
                entyFormat2.Entry.IsEnabled = PModel.EnableFomat_2 = false;
                entyFormat3.Entry.IsEnabled =  PModel.EnableFomat_3 = false;
                PModel.YourContractNumber = $"{strYearFormat_1} - {PModel.Format_1} - {strTimeFormat_1}";
            }
            else if (sender == rbFormat2)
            { 
                rbFormat1.TextColor = Color.Black;
                rbFormat2.TextColor = Color.White;
                rbFormat3.TextColor = Color.Black;
              
                lbFormat1_1.TextColor = Color.Gray;
                lbFormat1_2.TextColor = Color.Gray;
                lbFormat2_1.TextColor = Color.White;
                lbFormat2_2.TextColor = Color.White;
                lbFormat3_1.TextColor = Color.Gray;
                lbFormat3_2.TextColor = Color.Gray;

                entyFormat1.Entry.IsEnabled = PModel.EnableFomat_1 = false;
                entyFormat2.Entry.IsEnabled = PModel.EnableFomat_2 = true;
                entyFormat3.Entry.IsEnabled = PModel.EnableFomat_3 = false;
                PModel.YourContractNumber = $"{PModel.Format_2} - {strYearFormat_2} - {strTimeFormat_2}";
            }
            else if (sender == rbFormat3)
            {
                rbFormat1.TextColor = Color.Black;
                rbFormat2.TextColor = Color.Black;
                rbFormat3.TextColor = Color.White;

                lbFormat1_1.TextColor = Color.Gray;
                lbFormat1_2.TextColor = Color.Gray;
                lbFormat2_1.TextColor = Color.Gray;
                lbFormat2_2.TextColor = Color.Gray;
                lbFormat3_1.TextColor = Color.White;
                lbFormat3_2.TextColor = Color.White;

                entyFormat1.Entry.IsEnabled = PModel.EnableFomat_1 = false;
                entyFormat2.Entry.IsEnabled = PModel.EnableFomat_2 = false;
                entyFormat3.Entry.IsEnabled = PModel.EnableFomat_3 = true;
                PModel.YourContractNumber = $"{strYearFormat_3} - {strTimeFormat_3} - {PModel.Format_3}";
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
            if (sender == lbFormat1_1 || sender == lbFormat1_2 || sender == entyFormat1)
            {
                PModel.CheckFormat_1 = true;
            }
            else if (sender == lbFormat2_1 || sender == lbFormat2_2 || sender == entyFormat2)
            {
                PModel.CheckFormat_2 = true;
            }
            else if (sender == lbFormat3_1 || sender == lbFormat3_2 || sender == entyFormat3)
            {
                PModel.CheckFormat_3 = true;
            }
        }
    }
}