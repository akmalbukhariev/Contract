using LibContract.HttpModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibContract.Response
{
    public class ResponseApplicableContract : Response
    {
        public List<ApplicableContract> data { get; set; } = new List<ApplicableContract>();
    }
}
