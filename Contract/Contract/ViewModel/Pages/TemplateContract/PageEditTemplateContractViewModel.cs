﻿using LibContract.HttpModels;
using LibContract.HttpResponse;
using Contract.Model;
using Contract.Pages.TemplateContract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Contract.ViewModel.Pages.TemplateContract
{ 
    public class PageEditTemplateContractViewModel : BaseModel
    { 
        public bool Editable { get => GetValue<bool>(); set => SetValue(value); }
        public bool EnableAddUpdate { get => GetValue<bool>(); set => SetValue(value); }
        public string ContractNumberFormat { get => GetValue<string>(); set => SetValue(value); }
        public string AddressOfContract { get => GetValue<string>(); set => SetValue(value); }
        public string NameOfTemplate { get => GetValue<string>(); set => SetValue(value); }
        public string BtnEditDoneText { get => GetValue<string>(); set => SetValue(value); }
        public Model.ContractNumber SelectedContractNumberTemplate { get => GetValue<Model.ContractNumber>(); set => SetValue(value); }

        public LibContract.HttpModels.ContractTemplate TemplateInfo { get; set; } = null;
        public PageEditTemplateContractViewModel OldModel { get; set; } = null;
        public ObservableCollection<EditTemplate> ContractClausesList { get; set; }
        public ObservableCollection<Model.ContractNumber> ContractNumberTemplateList { get; set; }
        
        public PageEditTemplateContractViewModel(LibContract.HttpModels.ContractTemplate templateInfo, INavigation navigation) : base(navigation)
        {
            TemplateInfo = templateInfo;
            EnableAddUpdate = true;

            ContractClausesList = new ObservableCollection<EditTemplate>();
            ContractNumberTemplateList = new ObservableCollection<Model.ContractNumber>();

            ContractNumberFormat = RSC.SelectFormat;

            ItemDragged = new Command<EditTemplate>(OnItemDragged);
            ItemDraggedOver = new Command<EditTemplate>(OnItemDraggedOver);
            ItemDragLeave = new Command<EditTemplate>(OnItemDragLeave);
            ItemDropped = new Command<EditTemplate>(OnItemDropped);
            ItemDelete = new Command<EditTemplate>(DeleteItem);
            ItemEditText = new Command<EditTemplate>(EditItemText);

            Editable = false;
            BtnEditDoneText = RSC.Edit; 
        }

        public PageEditTemplateContractViewModel()
        {
            TemplateInfo = null;
            EnableAddUpdate = true;

            ContractClausesList = new ObservableCollection<EditTemplate>();
            ContractNumberTemplateList = new ObservableCollection<Model.ContractNumber>();

            ContractNumberFormat = RSC.SelectFormat;

            ItemDragged = new Command<EditTemplate>(OnItemDragged);
            ItemDraggedOver = new Command<EditTemplate>(OnItemDraggedOver);
            ItemDragLeave = new Command<EditTemplate>(OnItemDragLeave);
            ItemDropped = new Command<EditTemplate>(OnItemDropped);
            ItemDelete = new Command<EditTemplate>(DeleteItem);
            ItemEditText = new Command<EditTemplate>(EditItemText);

            Editable = false;
            BtnEditDoneText = RSC.Edit;
        }

        public PageEditTemplateContractViewModel(PageEditTemplateContractViewModel other)
        {
            Copy(other);
        }

        public void Copy(PageEditTemplateContractViewModel other)
        {
            Editable = other.Editable;
            EnableAddUpdate = other.EnableAddUpdate;
            ContractNumberFormat = other.ContractNumberFormat;
            AddressOfContract = other.AddressOfContract;
            NameOfTemplate = other.NameOfTemplate;

            if (other.SelectedContractNumberTemplate != null)
                SelectedContractNumberTemplate = new Model.ContractNumber(other.SelectedContractNumberTemplate);
            
            TemplateInfo = new LibContract.HttpModels.ContractTemplate(other.TemplateInfo);

            foreach (EditTemplate item in other.ContractClausesList)
            {
                ContractClausesList.Add(new EditTemplate(item));
            }

            foreach (Model.ContractNumber item in other.ContractNumberTemplateList)
            {
                ContractNumberTemplateList.Add(new Model.ContractNumber(item));
            }
        }

        public bool Equals(PageEditTemplateContractViewModel other)
        {
            bool res1 = SelectedContractNumberTemplate?.Id == other.SelectedContractNumberTemplate?.Id &&
                        NameOfTemplate.Equals(other.NameOfTemplate) &&
                        AddressOfContract.Equals(other.AddressOfContract);
            
            bool res2 = true;
            if (ContractClausesList.Count != other.ContractClausesList.Count)
            {
                res2 = false;
            }
            else
            {
                for (int i = 0; i < ContractClausesList.Count; i++)
                {
                    if (!ContractClausesList[i].Equals(other.ContractClausesList[i]))
                    {
                        res2 = false;
                        break;
                    }
                }
            }

            return res1 && res2;
        }
         
        public async void RequestInfo()
        {
            if (!ControlApp.InternetOk()) return;

            ControlApp.ShowLoadingView(RSC.PleaseWait);
            ResponseContractNumberTemplate response = await Net.HttpService.GetContractNumber(ControlApp.UserInfo.phone_number);
            ControlApp.CloseLoadingView();

            if (!ControlApp.CheckResponse(response)) return;

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

                    newItem.Id = item.id;
                    newItem.ContractNumberText = strTemplate;
                    newItem.Format = item.format;
                    newItem.CreatedDate = item.created_date; 

                    ContractNumberTemplateList.Add(newItem);
                }
            }

            if (TemplateInfo == null)
                DefaultTemplate();
            else
                EditTemplate(); 
        }
         
        #region Commands
        public ICommand ItemDragged { get; }
        public ICommand ItemDraggedOver { get; }
        public ICommand ItemDragLeave { get; }
        public ICommand ItemDropped { get; }
        public ICommand ItemEditText { get; }
        public ICommand ItemDelete { get; }

        public ICommand CommandShowClauseBox => new Command<EditTemplate>(ClickEditItem);
        public ICommand CommandCloseClauseBox => new Command(ClickBoxViewBack);
        public ICommand CommandSaveUpdate => new Command(SaveUpdate);
        public ICommand CommandEditDone => new Command(EditDone);
        public ICommand CommandAdd => new Command(Add);
        #endregion
         
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

            ContractClausesList.ForEach(item => item.Editable = Editable);
            EnableAddUpdate = !Editable;
        }

        private void Add()
        {
            EditTemplate newItem = new EditTemplate();
            newItem.Title = $"{ContractClausesList.Where(item => item.Title.Trim() != "").Count() + 1}";
            ContractClausesList.Add(newItem);
        }

        #region Drag and drop
        bool isDragged = false;
        private void OnItemDragged(EditTemplate item)
        { 
            ContractClausesList.ForEach(i => i.IsBeingDragged = item == i);
            if (!isDragged)
            {
                isDragged = true;
                ControlApp.Vibrate();
            }
        }

        private void OnItemDraggedOver(EditTemplate item)
        {
            isDragged = false;
            var itemBeingDragged = ContractClausesList.FirstOrDefault(i => i.IsBeingDragged);
            ContractClausesList.ForEach(i => i.IsBeingDraggedOver = item == i && item != itemBeingDragged);
        }

        private void OnItemDragLeave(EditTemplate item)
        { 
            ContractClausesList.ForEach(i => i.IsBeingDraggedOver = false);
            ControlApp.Vibrate();
        }

        private void OnItemDropped(EditTemplate item)
        {
            if (!Editable) return;

            var itemToMove = ContractClausesList.First(i => i.IsBeingDragged);
            var itemToInsertBefore = item;

            isDragged = false;

            if (itemToMove == null || itemToInsertBefore == null || itemToMove == itemToInsertBefore)
                return;

            ContractClausesList.Remove(itemToMove);
            var insertAtIndex = ContractClausesList.IndexOf(itemToInsertBefore);
            ContractClausesList.Insert(insertAtIndex, itemToMove);
            itemToMove.IsBeingDragged = false;
            itemToInsertBefore.IsBeingDraggedOver = false;

            Thread thread = new Thread(new ThreadStart(OrderItems));
            thread.IsBackground = true;
            thread.Start();
        }
        #endregion

        private async void EditItemText(EditTemplate item)
        {
            if (!Editable)
            { 
                await Navigation.PushAsync(new PageClausesChild(item));
            }
        }

        private async void DeleteItem(EditTemplate item)
        {
            if (await Application.Current.MainPage.DisplayAlert(RSC.ContractTemplates, $"{RSC.DeleteMessage} {item.Title}", RSC.Yes, RSC.No, FlowDirection.LeftToRight))
            {
                ContractClausesList.Remove(item);
                OrderItems();
            }
        }

        private void OrderItems()
        {
            int count = 0;
            foreach (EditTemplate item in ContractClausesList)
            {
                if (item.Title.Trim() != "")
                {
                    count++;
                    item.Title = $"{count}";

                    int chCount = 0;
                    foreach (EditTemplate childItem in item.Child)
                    {
                        chCount++;
                        childItem.Title = $"{count}.{chCount}"; 
                    }
                }
            }
        }
        
        void ClickEditItem(EditTemplate item)
        {
            //ShowClauseBox = true;
        }

        void ClickBoxViewBack()
        {
            //ShowClauseBox = false;
        }

        public void Update(EditTemplate item)
        { 
            for (int i = 0; i < ContractClausesList.Count; i++)
            {
                if (ContractClausesList[i].Title.Trim().Equals(item.Title.Trim()))
                {
                    if (item.IsDeleted)
                    {
                        ContractClausesList.RemoveAt(i);
                        break;
                    }
                    else
                    {
                        ContractClausesList[i] = new EditTemplate(item);
                        break;
                    }
                }
            }
        }

        public async void SaveUpdate()
        {
            if (!ControlApp.InternetOk()) return;
            
            if (SelectedContractNumberTemplate == null || string.IsNullOrEmpty(NameOfTemplate) || ContractClausesList.Count == 0)
            {
                await Application.Current.MainPage.DisplayAlert(RSC.Templates, RSC.FieldEmpty, RSC.Ok);
                return;
            }
            
            string strJson = JsonConvert.SerializeObject(TemplateToJson());
            LibContract.HttpModels.ContractTemplate data = new LibContract.HttpModels.ContractTemplate()
            {
                user_phone_number = ControlApp.UserInfo.phone_number,
                contract_number_format_id = SelectedContractNumberTemplate.Id,
                contract_number_option = SelectedContractNumberTemplate.ContractNumberText.Replace(Constants.ContractSequenceNumber,"").Replace("-",""),
                contract_address = AddressOfContract,
                template_name = NameOfTemplate,
                clauses = strJson,
                id = TemplateInfo != null? TemplateInfo.id : 0,
                created_date = TemplateInfo != null ? TemplateInfo.created_date : DateTime.Now.ToString(Constants.TimeFormat)
            };

            string strMessage = "";
            if (TemplateInfo == null)
            {
                ControlApp.ShowLoadingView(RSC.PleaseWait);
                ResponseContractTemplate response = await Net.HttpService.SetContractTemplate(data);
                ControlApp.CloseLoadingView();

                if (!ControlApp.CheckResponse(response)) return;
                
                strMessage = response.result ? RSC.SuccessfullyCompleted : RSC.Failed;

                await Application.Current.MainPage.DisplayAlert(RSC.Templates, strMessage, RSC.Ok);
                await Navigation.PopAsync();
            }
            else if (OldModel !=null && !OldModel.Equals(this))
            {
                ControlApp.ShowLoadingView(RSC.PleaseWait);
                ResponseContractTemplate response = await Net.HttpService.UpdateContractTemplate(data);
                ControlApp.CloseLoadingView();

                if (!ControlApp.CheckResponse(response)) return;
                
                strMessage = response.result ? RSC.SuccessfullyUpdated : RSC.Failed;

                await Application.Current.MainPage.DisplayAlert(RSC.Templates, strMessage, RSC.Ok);
                await Navigation.PopAsync();
            }
        }

        private void DefaultTemplate()
        {
            //ResponseReadyTemplate response = await Net.HttpService.GetAllReadyTemplate();
            //if (response.result)
            //{
            //ReadyTemplate found = response.data.Where(item => item.id.Equals(ControlApp.UserInfo.default_template_id)).FirstOrDefault();
            //    if (found != null)
            //        JsonToTemplate(found.clauses);
            //}
            JsonToTemplate(Constants.Template_New);
        }

        private void EditTemplate()
        {
            SelectedContractNumberTemplate = ContractNumberTemplateList.Where(item => item.Id == TemplateInfo.contract_number_format_id).FirstOrDefault();
            AddressOfContract = TemplateInfo.contract_address;
            NameOfTemplate = TemplateInfo.template_name;

            ContractClausesList.Clear();
            JsonToTemplate(TemplateInfo.clauses);

            OldModel = new PageEditTemplateContractViewModel();
            OldModel.Copy(this);

            ControlApp.CloseLoadingView();
        }

        private void JsonToTemplate(string clausesJson)
        {     
            List<ContractTemplateJson> rList = JsonConvert.DeserializeObject<List<ContractTemplateJson>>(clausesJson);
            foreach (ContractTemplateJson item in rList)
            {
                EditTemplate newItem = new EditTemplate();
                newItem.IsVisibleButton = item.IsButton;
                newItem.IsVisibleAddButton = item.IsVisibleAddButton;
                newItem.IsVisibleDeleteButton = !item.IsVisibleAddButton;
                newItem.IsContractInfoButton = item.IsContractInfoButton;
                newItem.IsContractServiceDetailsButton = item.IsContractServiceDetailsButton;
                newItem.Title = item.Title;
                newItem.Description = item.Description;
                newItem.IsVisibleItemClause = !item.IsButton;
                newItem.IsVisibleItemClauseDelete = false;
                 
                if (item.IsContractServiceDetailsButton)
                {
                    newItem.ButtonText = RSC.Info4;
                    newItem.ButtonDeleteText = RSC.Info6;
                }
                else if (item.IsContractInfoButton)
                {
                    newItem.ButtonText = RSC.Info5;
                    newItem.ButtonDeleteText = RSC.Info7;
                }

                foreach (ContractTemplateJson chItem in item.Child)
                {
                    EditTemplate newChItem = new EditTemplate();
                    newChItem.Title = chItem.Title;
                    newChItem.Description = chItem.Description;
                    
                    newItem.Child.Add(newChItem);
                }

                ContractClausesList.Add(newItem);
            } 
        }

        private List<ContractTemplateJson> TemplateToJson()
        {
            List<ContractTemplateJson> rList = new List<ContractTemplateJson>();

            foreach (EditTemplate item in ContractClausesList)
            {
                ContractTemplateJson newItem = item.Convert2ContractTemplateJson();

                foreach (EditTemplate chItem in item.Child)
                {
                    newItem.Child.Add(chItem.Convert2ContractTemplateJson());
                }

                rList.Add(new ContractTemplateJson(newItem));
            }
             
            return rList;
        }
    }
}