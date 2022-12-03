using LibContract.HttpModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibContract.HttpResponse
{
    public class ResponseCanceledContract : Response<List<CreateContractInfo>>, IResponse
    {
    }
}
