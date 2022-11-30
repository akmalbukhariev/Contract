
using Contract.HttpModels;
using Contract.HttpResponse;
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

        public async void RequestInfo()
        {
            DataList.Clear();

            if (!ControlApp.InternetOk()) return;

            ControlApp.ShowLoadingView(RSC.PleaseWait);
            ResponseContractNumberTemplate response = await Net.HttpService.GetContractNumber(ControlApp.UserInfo.phone_number);
            ControlApp.CloseLoadingView();

            if (response.result)
            {
                int count = 0;
                Color rowColor = Color.White;
                foreach (ContractNumberTemplate item in response.data)
                {
                    count++;
                    Model.ContractNumber newItem = new Model.ContractNumber();

                    if (count % 2 == 0)
                    {
                        rowColor = Color.FromHex("#DEEAF6");
                    }
                    else
                    {
                        rowColor = Color.White;
                    }

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
                    newItem.IsDeleted = item.is_deleted;

                    Add(newItem);
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
