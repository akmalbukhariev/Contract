using Contract.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Contract.Model
{
    public class EditTemplate : BaseModel
    {
        #region Properties
        public bool Editable { get => GetValue<bool>(); set => SetValue(value); }
        public bool IsVisibleDelete { get => GetValue<bool>(); set => SetValue(value); }
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
        #endregion

        public List<EditTemplate> Child { get => GetValue<List<EditTemplate>>(); set => SetValue(value);}

        public EditTemplate()
        {
            Editable = false;
            IsVisibleItemClause = true;
            IsVisibleDelete = false;
            IsThisAddClauseButton = false;
            IsVisibleAddContractInfoButton = false;
            IsVisibleAddClauseButton = false;
            IsVisibleAddDetailOfNegotiatorButton = false;

            Title = "";
            IsVisibleButton = false;
            ButtonColor = Color.FromHex("#5BAB42");

            Child = new List<EditTemplate>();
        }

        public EditTemplate(EditTemplate other)
        {
            Copy(other);
        }

        public void Copy(EditTemplate other)
        {
            Editable = other.Editable;
            IsBeingDragged = other.IsBeingDragged;
            IsBeingDraggedOver = other.IsBeingDraggedOver;

            IsVisibleItemClause = other.IsVisibleItemClause;
            IsVisibleDeleteButton = other.IsVisibleDeleteButton;
            IsVisibleAddButton = other.IsVisibleAddButton;

            IsThisAddClauseButton = other.IsThisAddClauseButton;

            IsVisibleAddContractInfoButton = other.IsVisibleAddContractInfoButton;
            IsVisibleAddClauseButton = other.IsVisibleAddClauseButton;
            IsVisibleAddDetailOfNegotiatorButton = other.IsVisibleAddDetailOfNegotiatorButton;

            IsVisibleButton = other.IsVisibleButton;

            ButtonText = other.ButtonText;
            ButtonDeleteText = other.ButtonDeleteText;
            ButtonColor = other.ButtonColor;

            Title = other.Title;
            Description = other.Description;
        }
    }
}
