using ContractAPI.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.Helper
{
    public abstract class AppBaseService
    {
        protected ContractMakerContext dataBase { get; set; }
    }
}
