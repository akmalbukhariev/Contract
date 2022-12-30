using System;
using System.Collections.Generic;
using System.Text;

namespace LibContract.HttpModels
{
    public class ReadyTemplate
    {
        public int id { get; set; }
        public string template_name_en { get; set; }
        public string template_name_ru { get; set; }
        public string template_name_uz { get; set; }
        public string template_name_uz_cyrl { get; set; }
        public string clauses { get; set; }
        public string file_url { get; set; }

        public ReadyTemplate()
        {
            id = 0;
            template_name_en = "";
            template_name_ru = "";
            template_name_uz = "";
            template_name_uz_cyrl = "";
            clauses = "";
            file_url = "";
        }

        public ReadyTemplate(ReadyTemplate other)
        {
            Copy(other);
        }

        public void Copy(ReadyTemplate other)
        {
            id = other.id;
            template_name_en = other.template_name_en;
            template_name_ru = other.template_name_ru;
            template_name_uz = other.template_name_uz;
            template_name_uz_cyrl = other.template_name_uz_cyrl;
            clauses = other.clauses;
            file_url = other.file_url;
        }
    }
}
