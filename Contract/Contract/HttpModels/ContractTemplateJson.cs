using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.HttpModels
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
            
        }

        public ContractTemplateJson(Model.EditTemplate template)
        {
            IsButton = template.IsVisibleButton;
            IsVisibleAddButton = template.IsVisibleAddButton;
            IsContractServiceDetailsButton = template.IsContractServiceDetailsButton;
            IsContractInfoButton = template.IsContractInfoButton; 
            Title = template.Title;
            Description = template.Description;

            Child = new List<ContractTemplateJson>();
            foreach (Model.EditTemplate item in template.Child)
            {
                Child.Add(new ContractTemplateJson(item));
            }
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
