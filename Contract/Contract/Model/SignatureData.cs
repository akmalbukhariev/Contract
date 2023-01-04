using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Contract.Model
{
    public class SignatureData
    {
        public string phone_number { get; set; }
        public Stream dataStream { get; set; }
    }
}
