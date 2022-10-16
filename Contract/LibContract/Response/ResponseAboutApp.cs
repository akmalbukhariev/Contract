using LibContract.HttpModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibContract.Response
{
    public class ResponseAboutApp : Response
    {
        public AboutApp data { get; set; } = new AboutApp();
    }
}
