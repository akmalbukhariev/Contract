using Contract.HttpModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.HttpResponse
{
    public class ResponseReadyTemplate : Response<List<ReadyTemplate>>, IResponse
    {
    }
}
