using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ZHS.Nrules.Core.Models;

namespace ZHS.Nrules.Infrastructure.Repository
{
    public interface IRepository<TEntity,Tkey > where TEntity  : IEntity<Tkey>
    {
    }
    public interface ICRUDRepository<TEntity,Tkey >:IRepository<TEntity,Tkey > where TEntity  : IEntity<Tkey>
    {
        TEntity Insert(TEntity t);

        TEntity InsertOrUpdate(TEntity t);

        void Delete(Tkey id);

        void Update(Tkey id,TEntity entity);

        TEntity Get(Tkey id);

        List<TEntity> FindList(Func<TEntity, bool> @where);

        IQueryable<TEntity> Queryable();
    }
}
