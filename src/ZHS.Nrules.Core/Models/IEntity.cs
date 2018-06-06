using System;

namespace ZHS.Nrules.Core.Models
{
    public  interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }
}