﻿using LibContract.HttpResponse;
using Contract.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contract.ViewModel.Pages.TemplateContract
{
    public class PageContractTemplateTableViewModel : BaseModel
    {
        public bool IsRefreshing { get => GetValue<bool>(); set => SetValue(value); }
         
        public ObservableCollection<ContractTemplate> DataList { get; set; }
        public PageContractTemplateTableViewModel()
        {
            DataList = new ObservableCollection<ContractTemplate>();
        }

        public ICommand RefreshCommand => new Command(Refresh);

        private void Refresh()
        {
            RequestInfo();
        }

        public async void RequestInfo()
        {
            if (!ControlApp.InternetOk()) return;

            ControlApp.ShowLoadingView(RSC.PleaseWait);
            ResponseContractTemplate response = await Net.HttpService.GetContractTemplate(ControlApp.UserInfo.phone_number);
            ControlApp.CloseLoadingView();

            DataList.Clear();
            if (response.result)
            {
                int count = 1;
                Color rowColor = Color.White;
                foreach (LibContract.HttpModels.ContractTemplate item in response.data)
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
                        TemplateInfo = new LibContract.HttpModels.ContractTemplate(item)
                    };

                    Add(newItem);
                    count++;
                }
            }

            IsRefreshing = false;
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
