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
        public string lan_id { get; set; }
        public string token { get; set; }
        /// <summary>
        /// 1 = yes, 2 = no
        /// </summary>
        public int on_notification { get; set; }
         
        public User()
        {
            //phone_number = "";
            //password = "";
            //reg_date = "";
            //default_template_id = 0;
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
            lan_id = other.lan_id;
            on_notification = other.on_notification;
        }
    }
}
