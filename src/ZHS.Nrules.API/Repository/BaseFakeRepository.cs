using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ZHS.Nrules.Core.Models;
using ZHS.Nrules.Infrastructure.Repository;

namespace ZHS.Nrules.API.Repository
{
    public class BaseFakeRepository<TEntity, Tkey> : List<TEntity>, IRepository<TEntity, Tkey> where TEntity : IEntity<Tkey>
    {
        public virtual TEntity Insert(TEntity t)
        {
            this.Add(t);
            return t;
        }

        public virtual void Delete(Tkey id)
        {
            this.Remove(Get(id));
        }

        public virtual void Update(Tkey id, TEntity entity)
        {
            entity.Id = id;
            this.Remove(Get(id));
            Insert(entity);
        }

        public virtual TEntity Get(Tkey id)
        {
            return this.FirstOrDefault(i => i.Id.Equals(id));
        }

        public virtual List<TEntity> FindList(Func<TEntity, bool> @where)
        {
            return this.Where(@where).ToList();
        }

        public virtual IQueryable<TEntity> Queryable()
        {
            return this.AsQueryable();
        }
    }
}