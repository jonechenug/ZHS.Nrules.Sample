using System;
using ZHS.Nrules.Core.Models.Orders;
using ZHS.Nrules.Core.Models.Promotion;

namespace ZHS.Nrules.Infrastructure.Repository
{
    public interface IBasePromotionRepository:ICRUDRepository<IBasePromotion,String >
    {
        
    }
}