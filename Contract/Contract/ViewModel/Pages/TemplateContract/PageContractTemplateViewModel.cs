using Contract.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace Contract.ViewModel.Pages.TemplateContract
{
    public class PageContractTemplateViewModel : BaseModel
    {
        public ObservableCollection<ContractTemplate> DataList { get; set; }
        public PageContractTemplateViewModel()
        {
            DataList = new ObservableCollection<ContractTemplate>();
        }

        public void Init()
        {
            ContractTemplate item1 = new ContractTemplate()
            {
                No = "1",
                ContractTempName = "Xizmat ko'rsatish bo'yicha \n dasturningodatiy shabloni",
                ContractPurpose = "Xizmat",
                ItemColor = Color.FromHex("#DEEAF6")
            };
            ContractTemplate item2 = new ContractTemplate()
            {
                No = "2",
                ContractTempName = "Xizmat ko'rsatish Samarqand",
                ContractPurpose = "Xizmat",
                ItemColor = Color.White
            };
            ContractTemplate item3 = new ContractTemplate()
            {
                No = "3",
                ContractTempName = "Tovarlar bo'yicha dasturning \n odatiy shabloni",
                ContractPurpose = "Tovar",
                ItemColor = Color.FromHex("#DEEAF6")
            };

            Add(item1);
            Add(item2);
            Add(item3);
        }

        public void Add(ContractTemplate item)
        {
            DataList.Add(item);
        }

        public void Remove(ContractTemplate item)
        {
            DataList.Remove(item);
        }
    }
}
