using ContractAPI.Models;
using LibContract.HttpModels;
using LibContract.HttpResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.Signature.service
{
    public interface ISignatureService
    {
        Task<ResponseSignatureInfo> setSignature(SignatureWithFile info);
    }
}
