using System;
using System.Collections.Generic;
using System.Text;

namespace ZHS.Nrules.Core.Models.Orders
{
    public enum OrderStatus
    {
        Submitted,
        AwaitingValidation,
        StockConfirmed,
        Paid,
        Shipped,
        Cancelled
    }
}
