using System;
using System.Collections.Generic;
using System.Text;

namespace LibContract.HttpModels
{
    public class ContractTemplateJson
    {
        public bool IsButton { get; set; }
        public bool IsVisibleAddButton { get; set; }
        public bool IsContractServiceDetailsButton { get; set; }
        public bool IsContractInfoButton { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<ContractTemplateJson> Child { get; set; }

        public ContractTemplateJson()
        {
            IsButton = false;
            IsVisibleAddButton = false;
            IsContractInfoButton = false;
            IsContractServiceDetailsButton = false;
            Title = "";
            Description = "";
            Child = new List<ContractTemplateJson>();
        }

        public ContractTemplateJson(ContractTemplateJson other)
        {
            Copy(other);
        }

        public void Copy(ContractTemplateJson other)
        {
            IsButton = other.IsButton;
            IsVisibleAddButton = other.IsVisibleAddButton;
            IsContractServiceDetailsButton = other.IsContractServiceDetailsButton;
            IsContractInfoButton = other.IsContractInfoButton;
            Title = other.Title;
            Description = other.Description;

            Child = new List<ContractTemplateJson>();
            foreach (ContractTemplateJson item in other.Child)
            {
                Child.Add(new ContractTemplateJson(item));
            }
        }
    }
}
