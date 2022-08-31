using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractAPI.Models
{
    public class User
    { 
        public int id { get; set; }
        public string phone_number { get; set; }
        public string password { get; set; }
        public string reg_date { get; set; }
    }
}
