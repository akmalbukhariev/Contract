using Contract.HttpResponse;
using Contract.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace Contract.ViewModel.Pages.TemplateContract
{
    public class PageContractTemplateTableViewModel : BaseModel
    {
        public ObservableCollection<ContractTemplate> DataList { get; set; }
        public PageContractTemplateTableViewModel()
        {
            DataList = new ObservableCollection<ContractTemplate>();
        }
         
        public async void RequestInfo()
        {
            if (!ControlApp.InternetOk()) return;

            ControlApp.ShowLoadingView(RSC.PleaseWait);
            ResponseContractTemplate response = await Net.HttpService.GetContractTemplate("12");
            ControlApp.CloseLoadingView();

            DataList.Clear();
            if (response.result)
            {
                int count = 1;
                Color rowColor = Color.White;
                foreach (HttpModels.ContractTemplate item in response.data)
                {
                    if (count % 2 == 0)
                    {
                        rowColor = Color.FromHex("#DEEAF6");
                    }
                    else
                    {
                        rowColor = Color.White;
                    }

                    ContractTemplate newItem = new ContractTemplate()
                    {
                        No = $"{count}",
                        ContractTempName = item.template_name,
                        ContractPurpose = RSC.Goods,
                        ItemColor = rowColor,
                        TemplateInfo = new HttpModels.ContractTemplate(item)
                    };

                    Add(newItem);
                    count++;
                }
            }
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
