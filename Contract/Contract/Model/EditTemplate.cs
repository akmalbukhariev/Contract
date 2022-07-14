using Contract.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Contract.Model
{
    public class EditTemplate : BaseModel
    {
        public bool IsBeingDragged { get => GetValue<bool>(); set => SetValue(value); }
        public bool IsBeingDraggedOver { get => GetValue<bool>(); set => SetValue(value); }

        public bool IsVisibleItemClause { get => GetValue<bool>(); set => SetValue(value); }
        public bool IsVisibleDeleteButton { get => GetValue<bool>(); set => SetValue(value); }
        public bool IsVisibleAddButton { get => GetValue<bool>(); set => SetValue(value); }

        public bool IsThisAddClauseButton { get => GetValue<bool>(); set => SetValue(value); }

        public bool IsVisibleAddContractInfoButton { get => GetValue<bool>(); set => SetValue(value); }
        public bool IsVisibleAddClauseButton { get => GetValue<bool>(); set => SetValue(value); }  
        public bool IsVisibleAddDetailOfNegotiatorButton { get => GetValue<bool>(); set => SetValue(value); }

        public bool IsVisibleButton { get => GetValue<bool>(); set => SetValue(value); }

        public string ButtonText { get => GetValue<string>(); set => SetValue(value); }
        public string ButtonDeleteText { get => GetValue<string>(); set => SetValue(value); }
        public Color ButtonColor { get => GetValue<Color>(); set => SetValue(value); }

        public string Title { get => GetValue<string>(); set => SetValue(value); }
        public string Description { get => GetValue<string>(); set => SetValue(value); }

        public EditTemplate()
        {
            IsVisibleItemClause = true;
            IsThisAddClauseButton = false;
            IsVisibleAddContractInfoButton = false;
            IsVisibleAddClauseButton = false;
            IsVisibleAddDetailOfNegotiatorButton = false;

            IsVisibleButton = false;
            ButtonColor = Color.FromHex("#5BAB42");
        }
    }
}
