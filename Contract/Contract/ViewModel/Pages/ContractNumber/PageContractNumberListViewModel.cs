
using LibContract.HttpModels;
using LibContract.HttpResponse;
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
         
        public async void RequestInfo()
        {
            DataList.Clear();

            if (!ControlApp.InternetOk()) return;

            ControlApp.ShowLoadingView(RSC.PleaseWait);
            ResponseContractNumberTemplate response = await Net.HttpService.GetContractNumber(ControlApp.UserInfo.phone_number);
            ControlApp.CloseLoadingView();

            if (!ControlApp.CheckResponse(response)) return;

            if (response.result)
            {
                int count = 0;
                Color rowColor = Color.White;
                foreach (ContractNumberTemplate item in response.data)
                {
                    count++;
                    Model.ContractNumber newItem = new Model.ContractNumber();

                    rowColor = count % 2 == 0 ? Color.FromHex("#DEEAF6") : Color.White;
                      
                    string strTemplate = "";
                    switch (item.format)
                    {
                        case 1:
                            strTemplate = Constants.ContractSequenceNumber;
                            break;
                        case 2:
                            strTemplate = $"{item.option}-{Constants.ContractSequenceNumber}";
                            break;
                        case 3:
                            strTemplate = $"{Constants.ContractSequenceNumber}-{item.option}";
                            break;
                        default:
                            break;
                    }

                    newItem.Id = item.id;
                    newItem.No = count.ToString();
                    newItem.ContractNumberText = strTemplate;
                    newItem.ItemColor = rowColor;
                    newItem.Format = item.format;
                    newItem.CreatedDate = item.created_date;
                    newItem.CancelImage = response.data.Count == 1 ? "cancel_disable" : "cancel";
                    newItem.IsEnabled = response.data.Count == 1 ? false : true;

                    Add(newItem);
                }

                if (DataList.Count > 0)
                {
                    ShowEmptyMessage = false;
                    CloseEmptyMessage = true;
                }
                else
                {
                    ShowEmptyMessage = true;
                    CloseEmptyMessage = false;
                }
            }
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
