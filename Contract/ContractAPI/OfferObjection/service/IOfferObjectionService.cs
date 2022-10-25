using Contract.HttpModels;
using Contract.HttpResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractAPI.OfferObjection.service
{
    public interface IOfferObjectionService
    {
        Task<ResponseOfferObjection> Save(OfferAndObjection info);
    }
}
