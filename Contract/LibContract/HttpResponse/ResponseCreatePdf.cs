using System;
using System.Collections.Generic;
using System.Text;

namespace LibContract.HttpResponse
{
    public class ResponseCreatePdf : Response, IResponse
    {
        public string pdf_url { get; set; }
    }
}
