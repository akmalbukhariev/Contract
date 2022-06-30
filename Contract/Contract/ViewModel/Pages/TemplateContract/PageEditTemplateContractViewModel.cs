using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Contract.ViewModel.Pages.TemplateContract
{
    public class TestDragAndDrop : BaseModel
    {
        public string image { get => GetValue<string>(); set => SetValue(value); }
        public string text { get => GetValue<string>(); set => SetValue(value); }
    }
    public class PageEditTemplateContractViewModel : BaseModel
    {
        public bool IsBeingDragged { get => GetValue<bool>(); set => SetValue(value); }
        public bool IsBeingDraggedOver { get => GetValue<bool>(); set => SetValue(value); }
        public ObservableCollection<TestDragAndDrop> DataList { get; set; }
        
        public PageEditTemplateContractViewModel()
        {
            DataList = new ObservableCollection<TestDragAndDrop>();
        }
    }
}
