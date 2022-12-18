using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibContract.HttpModels
{
    public class User
    {  
        public string phone_number { get; set; }
        public string password { get; set; }
        public string reg_date { get; set; }
        public int default_template_id { get; set; }

        public User()
        {
            
        }

        public User(User other)
        {
            Copy(other);
        }

        public void Copy(User other)
        { 
            phone_number = other.phone_number;
            password = other.password;
            reg_date = other.reg_date;
            default_template_id = other.default_template_id;
        }
    }
}
