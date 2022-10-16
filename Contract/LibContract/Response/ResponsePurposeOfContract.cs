using LibContract.HttpModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibContract.Response
{
    public class ResponsePurposeOfContract : Response
    {
        public PurposeOfContract data { get; set; } = new PurposeOfContract();
    }
}
