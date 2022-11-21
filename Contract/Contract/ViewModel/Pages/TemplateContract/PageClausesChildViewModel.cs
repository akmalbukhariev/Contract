using Contract.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Contract.ViewModel.Pages.TemplateContract
{
    public class PageClausesChildViewModel : BaseModel
    {
        public bool Editable { get => GetValue<bool>(); set => SetValue(value); }
        public string BtnEditDoneText { get => GetValue<string>(); set => SetValue(value); }

        private EditTemplate SelectedItem;
        public ObservableCollection<EditTemplate> DataList { get; set; }

        public PageClausesChildViewModel(EditTemplate selectedItem)
        {
            SelectedItem = selectedItem;
            DataList = new ObservableCollection<EditTemplate>();

            ItemDragged = new Command<EditTemplate>(OnItemDragged);
            ItemDraggedOver = new Command<EditTemplate>(OnItemDraggedOver);
            ItemDragLeave = new Command<EditTemplate>(OnItemDragLeave);
            ItemDropped = new Command<EditTemplate>(OnItemDropped);
            ItemDelete = new Command<EditTemplate>(DeleteItem);
            ItemClickEditText = new Command<EditTemplate>(ClickEditText);


            BtnEditDoneText = RSC.Edit;

            DataList.Add(new EditTemplate(selectedItem));

            foreach (EditTemplate item in selectedItem.Child)
            {
                DataList.Add(new EditTemplate(item));
            }
        }

        #region Commands
        public ICommand ItemDragged { get; }

        public ICommand ItemDraggedOver { get; }

        public ICommand ItemDragLeave { get; }

        public ICommand ItemDropped { get; }
        public ICommand ItemDelete { get; }
        public ICommand ItemClickEditText { get; }
        public ICommand CommandSave => new Command(Save);
        public ICommand CommandAdd => new Command(Add);
        public ICommand CommandEditDone => new Command(EditDone);
        #endregion

        private void Add()
        {
            EditTemplate newItem = new EditTemplate();
            newItem.Title = $"{DataList[0].Title}.{DataList.Count}";
            DataList.Add(newItem);
        }

        private void Save()
        {
            
        }
          
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

            Thread thread = new Thread(new ThreadStart(OrderItems));
            thread.IsBackground = true;
            thread.Start();
        }

        private void OrderItems()
        {
            int count = 0;
            foreach (EditTemplate item in DataList)
            { 
                item.Title = count == 0 ? SelectedItem.Title : $"{SelectedItem.Title}.{count}";
                count++;
            }
        }

        private async void DeleteItem(EditTemplate item)
        {
            if (await Application.Current.MainPage.DisplayAlert(RSC.ContractTemplates, $"{RSC.DeleteMessage} {item.Title}", RSC.Ok, RSC.Cancel, FlowDirection.LeftToRight))
            {
                DataList.Remove(item);
                OrderItems();
            }
        }

        private void ClickEditText(EditTemplate item)
        {
            
        }
    }
}
