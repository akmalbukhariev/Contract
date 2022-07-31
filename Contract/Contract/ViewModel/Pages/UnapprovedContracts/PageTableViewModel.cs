﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Contract.Model;
using Contract.Pages.UnapprovedContracts;
using Xamarin.Forms;

namespace Contract.ViewModel.Pages.UnapprovedContracts
{ 
    public class PageTableViewModel : BaseModel
    {
        public bool ShowConfirmBox { get => GetValue<bool>(); set => SetValue(value); }

        public ObservableCollection<UnapprovedContract> DataList { get; set; }

        public PageTableViewModel(INavigation navigation) : base(navigation)
        { 
            DataList = new ObservableCollection<UnapprovedContract>(); 
        }

        #region Commands
        public ICommand CommandCode => new Command(ClickCode);
        public ICommand CommandERI => new Command(ClickERI);
        public ICommand CommandBoxView => new Command(ClickBoxView);

        private async void ClickCode()
        {
            await Task.Delay(100);
            ShowConfirmBox = false;

            SetTransitionType(TransitionType.SlideFromBottom);
            await Navigation.PushModalAsync(new PageContractSpecialCode());
        }

        private async void ClickERI()
        {
            await Task.Delay(100);
            ShowConfirmBox = false;

            SetTransitionType(TransitionType.SlideFromBottom);
            await Navigation.PushModalAsync(new PageERIKey());
        }

        private void ClickBoxView()
        {
            ShowConfirmBox = false;
        }
        #endregion

        public void Init()
        {
            UnapprovedContract item1 = new UnapprovedContract()
            {
                No = "1.",
                Preparer = "Men",
                ContractNnumber = "22-001-12345",
                CompanyName = "Korxona nomi",
                ContractDate = "06.04.2022",
                ContractPrice = "100,000 sum",
                ItemColor = Color.FromHex("#DEEAF6"),
                PreparerColor = Color.FromHex("#BDD6EE")
            };
            UnapprovedContract item2 = new UnapprovedContract()
            {
                No = "2.",
                Preparer = "Kontragent",
                ContractNnumber = "22-001-12345",
                CompanyName = "Korxona nomi",
                ContractDate = "06.04.2022",
                ContractPrice = "100 $",
                ItemColor = Color.FromHex("#FFFFFF"),
                PreparerColor = Color.FromHex("#FFF2CC")
            };
            UnapprovedContract item3 = new UnapprovedContract()
            {
                No = "3.",
                Preparer = "Kontragent",
                ContractNnumber = "22-001-12345",
                CompanyName = "Korxona nomi",
                ContractDate = "06.04.2022",
                ContractPrice = "100 $",
                ItemColor = Color.FromHex("#DEEAF6"),
                PreparerColor = Color.FromHex("#BDD6EE")
            };

            Add(item1);
            Add(item2);
            Add(item3);
        }

        public void Add(UnapprovedContract item)
        {
            DataList.Add(item);
        }
    }
}
