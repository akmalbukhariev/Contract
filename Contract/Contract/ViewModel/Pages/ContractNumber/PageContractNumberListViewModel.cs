
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace Contract.ViewModel.Pages.ContractNumber
{
    public class PageContractNumberListViewModel : BaseModel
    {
        public ObservableCollection<Model.ContractNumber> DataList { get; set; }

        public PageContractNumberListViewModel()
        {
            DataList = new ObservableCollection<Model.ContractNumber>();
        }

        public void Init()
        {
            Model.ContractNumber item1 = new Model.ContractNumber()
            {
                No = "1",
                ContractNumberText = "YY-OO-XXXXXX",
                ItemColor = Color.FromHex("#DEEAF6")
            };
            Model.ContractNumber item2 = new Model.ContractNumber()
            {
                No = "2",
                ContractNumberText = "XXXXX",
                ItemColor = Color.White
            };
            Model.ContractNumber item3 = new Model.ContractNumber()
            {
                No = "3",
                ContractNumberText = "Sh-XXXXXX/YYS",
                ItemColor = Color.FromHex("#DEEAF6")
            };

            Add(item1);
            Add(item2);
            Add(item3);
        }

        private void Add(Model.ContractNumber item)
        {
            DataList.Add(item);
        }

        private void Remove(Model.ContractNumber item)
        {
            DataList.Remove(item);
        }
    }
}
