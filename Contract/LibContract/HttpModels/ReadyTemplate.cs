using System;
using System.Collections.Generic;
using System.Text;

namespace LibContract.HttpModels
{
    public class ReadyTemplate
    {
        public int id { get; set; }
        public string template_name { get; set; }
        public string clauses { get; set; }

        public ReadyTemplate()
        {
            id = 0;
            template_name = "";
            clauses = "";
        }

        public ReadyTemplate(ReadyTemplate other)
        {
            Copy(other);
        }

        public void Copy(ReadyTemplate other)
        {
            id = other.id;
            template_name = other.template_name;
            clauses = other.clauses;
        }
    }
}
