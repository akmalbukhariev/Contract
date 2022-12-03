using LibContract.HttpModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibContract.HttpResponse
{
    public class ResponseCreateContract : Response, IResponse
    {
         public string new_contract_sequence_number { get; set; }
    }
}
