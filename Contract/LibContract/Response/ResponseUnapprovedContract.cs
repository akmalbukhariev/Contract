using LibContract.HttpModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibContract.Response
{
    public class ResponseUnapprovedContract : Response
    {
        public List<UnapprovedContract> data { get; set; } = new List<UnapprovedContract>();
    }
}
