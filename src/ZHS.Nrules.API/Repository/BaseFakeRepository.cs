using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ZHS.Nrules.Core.Models;
using ZHS.Nrules.Infrastructure.Repository;

namespace ZHS.Nrules.API.Repository
{
    public class BaseFakeRepository<TEntity> : List<TEntity>, ICRUDRepository<TEntity, String> where TEntity : IEntity<String>
    {
        public virtual TEntity Insert(TEntity t)
        {
            this.Add(t);
            return t;
        }

        public virtual void Delete(String id)
        {
            this.Remove(Get(id));
        }

        public virtual void Update(String id, TEntity entity)
        {
            entity.Id = id;
            this.Remove(Get(id));
            Insert(entity);
        }

        public virtual TEntity Get(String id)
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

        public TEntity InsertOrUpdate(TEntity t)
        {
            if (String.IsNullOrWhiteSpace(t.Id))
            {
                t.Id = ZHS.Nrules.Infrastructure.Util.ObjectId.GenerateNewStringId();
            }
            else
            {
                var isExist=this.Count(i=>i.Id==t.Id)>0;
                if(isExist){
                    this.Update(t.Id,t);
                    return t;
                }
            }
            this.Insert(t);
            return t;
        }
    }
}