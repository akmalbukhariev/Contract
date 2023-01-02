using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LibContract.HttpModels
{
    public class SignatureInfo
    {
        public string fileName { get; set; }
        public Stream dataStream { get; set; }
        //public string urlFile { get; set; }
    }
}
