using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Model
{
    public class UnapprovedContract : BaseContract
    {
        public UnapprovedContract()
        {
            ShowBusy = false;
            ShowCheck = true;
        }
    }
}
