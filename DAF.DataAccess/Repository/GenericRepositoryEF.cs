using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DAF.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DAF.DataAccess.Repository
{
    public class GenericRepositoryEF<TEntity> : IRepository<TEntity> where TEntity : BaseModel
    {
        private DataDbContext Db { get; }

        public GenericRepositoryEF(DataDbContext db)
        {
            Db = db;
        }

        public IQueryable<TEntity> GetItems()
        {
            return Db.Set<TEntity>();
        }

        public IQueryable<TEntity> GetItems(Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> query = Db.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query;
        }

        public IQueryable<TEntity> GetItems(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
        {
            IQueryable<TEntity> query = Db.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query;
        }

        public TEntity GetItem(Guid id)
        {
            return Db.Set<TEntity>().Find(id);
        }

        public TEntity GetItem(Expression<Func<TEntity, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            return Db.Set<TEntity>().FirstOrDefault(filter);
        }

        public void Insert(TEntity item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            Db.Set<TEntity>().Add(item);
        }

        public void Insert(IEnumerable<TEntity> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            Db.Set<TEntity>().AddRange(items);
        }

        public void Update(TEntity item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            Db.Entry(item).State = EntityState.Modified;
        }

        public void Update(IEnumerable<TEntity> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            foreach (TEntity item in items)
            {
                Db.Entry(item).State = EntityState.Modified;
            }
        }

        public void UpdateByFields(TEntity item, IEnumerable<string> fieldNameArray)
        {
            Update(item);

            foreach (var fieldName in fieldNameArray)
            {
                Db.Entry(item).Property(fieldName).IsModified = true;
            }            
        }

        public void Delete(Guid id)
        {
            TEntity item = Db.Set<TEntity>().Find(id);

            if (item != null)
            {
                Db.Set<TEntity>().Remove(item);
            }
        }

        public void Delete(Guid[] ids)
        {
            IQueryable<TEntity> items = GetItems().Where(item => ids.Contains(item.Id));

            foreach (TEntity item in items)
            {
                Db.Set<TEntity>().Remove(item);
            }
        }

        public void Delete(TEntity item)
        {
            Db.Set<TEntity>().Remove(item);
        }

        public void Delete(IEnumerable<TEntity> items)
        {
            throw new NotImplementedException();
        }
    }
}