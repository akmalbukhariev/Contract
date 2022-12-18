using System;
using System.Collections.Generic;
using System.Text;

namespace LibContract.HttpModels
{
    public class ChnagePassword
    {
        public string phone_number { get; set; }
        public string password { get; set; }
        public string new_password { get; set; }

        public void Copy(ChnagePassword other)
        {
            this.phone_number = other.phone_number;
            this.password = other.password;
            this.new_password = other.new_password;
        }
    }
}
