using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

using Contract.Model;
using Xamarin.Forms;

namespace Contract.ViewModel.Pages.UnapprovedContracts
{ 
    public class PageTableViewModel : BaseModel
    {
        public ObservableCollection<UnapprovedContract> DataList { get; set; }

        public PageTableViewModel()
        { 
            DataList = new ObservableCollection<UnapprovedContract>(); 
        }

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
