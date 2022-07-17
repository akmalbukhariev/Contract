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
        public EditTemplate SelectedItem { get => GetValue<EditTemplate>(); set => SetValue(value); }
        public ObservableCollection<EditTemplate> DataList { get; set; }
        
        public PageEditTemplateContractViewModel()
        {
            DataList = new ObservableCollection<EditTemplate>();
             
            ItemDragged = new Command<EditTemplate>(OnItemDragged);
            ItemDraggedOver = new Command<EditTemplate>(OnItemDraggedOver);
            ItemDragLeave = new Command<EditTemplate>(OnItemDragLeave);
            ItemDropped = new Command<EditTemplate>(OnItemDropped);
            ItemEditText = new Command<EditTemplate>(EditItemText);
        }
         
        public ICommand ItemDragged { get; }

        public ICommand ItemDraggedOver { get; }

        public ICommand ItemDragLeave { get; }

        public ICommand ItemDropped { get; }

        public ICommand ItemEditText { get; }

        public ICommand CommandShowClauseBox => new Command(ClickEditItem);
        public ICommand CommandCloseClauseBox => new Command(ClickBoxViewBack);

        bool isDragged = false;
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
    }
}
