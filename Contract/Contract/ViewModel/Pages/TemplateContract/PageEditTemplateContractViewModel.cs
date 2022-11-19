using Contract.HttpModels;
using Contract.HttpResponse;
using Contract.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Contract.ViewModel.Pages.TemplateContract
{ 
    public class PageEditTemplateContractViewModel : BaseModel
    {
        public bool ShowClauseBox { get => GetValue<bool>(); set => SetValue(value); }
        public bool Editable { get => GetValue<bool>(); set => SetValue(value); }

        public string ContractNumberFormat { get => GetValue<string>(); set => SetValue(value); }
        public string AddressOfCompany { get => GetValue<string>(); set => SetValue(value); }
        public string NameOfTemplate { get => GetValue<string>(); set => SetValue(value); }
        public string BtnEditDoneText { get => GetValue<string>(); set => SetValue(value); }

        public Model.ContractNumber SelectedContractNumberTemplate { get => GetValue<Model.ContractNumber>(); set => SetValue(value); }
        public ObservableCollection<EditTemplate> DataList { get; set; }
        public ObservableCollection<Model.ContractNumber> ContractNumberTemplateList { get; set; }
        
        public PageEditTemplateContractViewModel()
        {
            DataList = new ObservableCollection<EditTemplate>();
            ContractNumberTemplateList = new ObservableCollection<Model.ContractNumber>();

            ContractNumberFormat = RSC.SelectFormat;

            ItemDragged = new Command<EditTemplate>(OnItemDragged);
            ItemDraggedOver = new Command<EditTemplate>(OnItemDraggedOver);
            ItemDragLeave = new Command<EditTemplate>(OnItemDragLeave);
            ItemDropped = new Command<EditTemplate>(OnItemDropped);
            ItemEditText = new Command<EditTemplate>(EditItemText);

            Editable = false;
            BtnEditDoneText = RSC.Edit;
        }

        public async void RequestInfo()
        {
            if (!ControlApp.InternetOk()) return;

            ControlApp.ShowLoadingView(RSC.PleaseWait);
            ResponseContractNumberTemplate response = await Net.HttpService.GetContractNumber("12");
            ControlApp.CloseLoadingView();

            if (response.result)
            {
                foreach (ContractNumberTemplate item in response.data)
                { 
                    Model.ContractNumber newItem = new Model.ContractNumber();
                     
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
                     
                    newItem.ContractNumberText = strTemplate;
                     
                    newItem.Format = item.format;
                    newItem.CreatedDate = item.created_date;
                    newItem.IsDeleted = item.is_deleted;

                    ContractNumberTemplateList.Add(newItem);
                }
            }
        }

        #region Commands
        public ICommand ItemDragged { get; }

        public ICommand ItemDraggedOver { get; }

        public ICommand ItemDragLeave { get; }

        public ICommand ItemDropped { get; }

        public ICommand ItemEditText { get; }

        public ICommand CommandShowClauseBox => new Command(ClickEditItem);
        public ICommand CommandCloseClauseBox => new Command(ClickBoxViewBack);
        public ICommand CommandSaveUpdate => new Command(SaveUpdate);
        public ICommand CommandEditDone => new Command(EditDone);
        #endregion

        bool isDragged = false;

        private void EditDone()
        {
            if (Editable)
            {
                Editable = false;
                BtnEditDoneText = RSC.Edit;
            }
            else
            {
                Editable = true;
                BtnEditDoneText = RSC.Done;
            }

            DataList.ForEach(item => item.Editable = Editable);
        }

        private void OnItemDragged(EditTemplate item)
        { 
            DataList.ForEach(i => i.IsBeingDragged = item == i);
            if (!isDragged)
            {
                isDragged = true;
                ControlApp.Vibrate();
            }
        }

        private void OnItemDraggedOver(EditTemplate item)
        { 
            var itemBeingDragged = DataList.FirstOrDefault(i => i.IsBeingDragged);
            DataList.ForEach(i => i.IsBeingDraggedOver = item == i && item != itemBeingDragged);
        }

        private void OnItemDragLeave(EditTemplate item)
        { 
            DataList.ForEach(i => i.IsBeingDraggedOver = false);
            ControlApp.Vibrate();
        }

        private void OnItemDropped(EditTemplate item)
        {
            if (!Editable) return;

            var itemToMove = DataList.First(i => i.IsBeingDragged);
            var itemToInsertBefore = item;

            isDragged = false;

            if (itemToMove == null || itemToInsertBefore == null || itemToMove == itemToInsertBefore)
                return;

            DataList.Remove(itemToMove);
            var insertAtIndex = DataList.IndexOf(itemToInsertBefore);
            DataList.Insert(insertAtIndex, itemToMove);
            itemToMove.IsBeingDragged = false;
            itemToInsertBefore.IsBeingDraggedOver = false;
        }

        private void EditItemText(EditTemplate item)
        {
            
        }

        void ClickEditItem()
        {
            ShowClauseBox = true;
        }

        void ClickBoxViewBack()
        {
            ShowClauseBox = false;
        }

        async void SaveUpdate()
        {
            
        }
    }
}
