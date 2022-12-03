using Contract.ViewModel;
using LibContract.HttpModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Contract.Model
{
    public class EditTemplate : BaseModel
    {
        #region Properties
        public bool IsDeleted { get => GetValue<bool>(); set => SetValue(value); }
        public bool Editable { get => GetValue<bool>(); set => SetValue(value); }
        public bool IsBeingDragged { get => GetValue<bool>(); set => SetValue(value); }
        public bool IsBeingDraggedOver { get => GetValue<bool>(); set => SetValue(value); }

        public bool IsVisibleItemClauseDelete { get => GetValue<bool>(); set => SetValue(value); }
        public bool IsVisibleItemClause { get => GetValue<bool>(); set => SetValue(value); }

        public bool IsVisibleDeleteButton { get => GetValue<bool>(); set => SetValue(value); }
        //public bool IsThisAddClauseButton { get => GetValue<bool>(); set => SetValue(value); }
        //public bool IsVisibleAddContractInfoButton { get => GetValue<bool>(); set => SetValue(value); }
        //public bool IsVisibleAddClauseButton { get => GetValue<bool>(); set => SetValue(value); }  
        //public bool IsVisibleAddDetailOfNegotiatorButton { get => GetValue<bool>(); set => SetValue(value); }

        public bool IsVisibleButton { get => GetValue<bool>(); set => SetValue(value); }
        public bool IsVisibleAddButton { get => GetValue<bool>(); set => SetValue(value); }
        public bool IsContractServiceDetailsButton { get => GetValue<bool>(); set => SetValue(value); }
        public bool IsContractInfoButton { get => GetValue<bool>(); set => SetValue(value); }

        public string ButtonText { get => GetValue<string>(); set => SetValue(value); }
        public string ButtonDeleteText { get => GetValue<string>(); set => SetValue(value); }
        public Color ButtonColor { get => GetValue<Color>(); set => SetValue(value); }

        public string Title { get => GetValue<string>(); set => SetValue(value); }
        public string Description { get => GetValue<string>(); set => SetValue(value); }
        #endregion

        public List<EditTemplate> Child { get => GetValue<List<EditTemplate>>(); set => SetValue(value);}

        public EditTemplate(bool isDeleted = false)
        {
            Editable = false;
            IsVisibleItemClause = true;
            IsVisibleItemClauseDelete = false;
            //IsThisAddClauseButton = false;
            //IsVisibleAddContractInfoButton = false;
            //IsVisibleAddClauseButton = false;
            //IsVisibleAddDetailOfNegotiatorButton = false;

            Title = "";
            Description = "";
            IsVisibleButton = false;
            ButtonColor = Color.FromHex("#5BAB42");

            Child = new List<EditTemplate>();

            IsDeleted = isDeleted;
        }

        public EditTemplate(EditTemplate other)
        {
            Copy(other);
        }

        public ContractTemplateJson Convert2ContractTemplateJson()
        {
            return new ContractTemplateJson()
            {
                IsButton = IsVisibleButton,
                IsVisibleAddButton = IsVisibleAddButton,
                IsContractServiceDetailsButton = IsContractServiceDetailsButton,
                IsContractInfoButton = IsContractInfoButton,
                Title = Title,
                Description = Description
            };
        }

        public void Copy(EditTemplate other)
        {
            Editable = other.Editable;
            IsBeingDragged = other.IsBeingDragged;
            IsBeingDraggedOver = other.IsBeingDraggedOver;

            IsVisibleItemClause = other.IsVisibleItemClause;
            //IsVisibleDeleteButton = other.IsVisibleDeleteButton;
            IsVisibleAddButton = other.IsVisibleAddButton;

            //IsThisAddClauseButton = other.IsThisAddClauseButton;

            //IsVisibleAddContractInfoButton = other.IsVisibleAddContractInfoButton;
            //IsVisibleAddClauseButton = other.IsVisibleAddClauseButton;
            //IsVisibleAddDetailOfNegotiatorButton = other.IsVisibleAddDetailOfNegotiatorButton;

            IsVisibleButton = other.IsVisibleButton;

            ButtonText = other.ButtonText;
            ButtonDeleteText = other.ButtonDeleteText;
            ButtonColor = other.ButtonColor;

            Title = other.Title;
            Description = other.Description;

            Child = new List<EditTemplate>();
            foreach (EditTemplate item in other.Child)
            {
                Child.Add(new EditTemplate(item));
            }
        }

        public bool Equals(EditTemplate other)
        {
            bool res1 = //Editable == other.Editable &&
                        //IsBeingDragged == other.IsBeingDragged &&
                        //IsBeingDraggedOver == other.IsBeingDraggedOver &&
                        //IsVisibleItemClause == other.IsVisibleItemClause &&
                        //IsVisibleDeleteButton == other.IsVisibleDeleteButton &&
                        IsVisibleAddButton == other.IsVisibleAddButton &&
                        //IsThisAddClauseButton == other.IsThisAddClauseButton &&
                        //IsVisibleAddContractInfoButton == other.IsVisibleAddContractInfoButton &&
                        //IsVisibleAddClauseButton == other.IsVisibleAddClauseButton &&
                        //IsVisibleAddDetailOfNegotiatorButton == other.IsVisibleAddDetailOfNegotiatorButton &&
                        //IsVisibleButton == other.IsVisibleButton &&
                        ButtonText == other.ButtonText &&
                        ButtonDeleteText == other.ButtonDeleteText &&
                        //ButtonColor == other.ButtonColor &&
                        Title == other.Title &&
                        Description == other.Description;

            bool res2 = true;
            if (Child.Count != other.Child.Count)
            {
                res2 = false;
            }
            else if(Child.Count == other.Child.Count)
            {
                for (int i = 0; i < Child.Count; i++)
                {
                    if (!Child[i].Equals(other.Child[i]))
                    {
                        res2 = false; break;
                    }
                }
            }

            return res1 && res2;
        }   
    }
}
