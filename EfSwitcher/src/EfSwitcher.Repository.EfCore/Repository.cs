using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EfSwitcher.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace EfSwitcher.Repository.EfCore
{
    public abstract class Repository<TEntity> : DbSet<TEntity>, IRepository<TEntity>
        where TEntity : class
    {
        public virtual IEnumerable<TEntity> Select(
            Expression<Func<TEntity, bool>> where = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includes = null,
            int? skipPage = null,
            int? takePage = null,
            int? skip = null,
            int? take = null,
            bool tracking = false)
        {
            IQueryable<TEntity> query = Query(where, orderBy, includes, skipPage, takePage, skip, take, tracking);
            return query.ToList();
        }

        public virtual IEnumerable<TResult> Select<TResult>(
            Expression<Func<TEntity, TResult>> select,
            Expression<Func<TEntity, bool>> where = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includes = null,
            int? skipPage = null,
            int? takePage = null,
            int? skip = null,
            int? take = null,
            bool tracking = false)
        {
            IQueryable<TEntity> query = Query(where, orderBy, includes, skipPage, takePage, skip, take, tracking);
            return query.Select(select).ToList();
        }

        public virtual TEntity FindFirst(
            Expression<Func<TEntity, bool>> where = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includes = null,
            int? skipPage = null,
            int? takePage = null,
            int? skip = null,
            int? take = null,
            bool tracking = false)
        {
            IQueryable<TEntity> query = Query(where, orderBy, includes, skipPage, takePage, skip, take, tracking);
            return query.FirstOrDefault();
        }

        public virtual TResult FindFirst<TResult>(
            Expression<Func<TEntity, TResult>> select,
            Expression<Func<TEntity, bool>> where = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includes = null,
            int? skipPage = null,
            int? takePage = null,
            int? skip = null,
            int? take = null,
            bool tracking = false)
        {
            IQueryable<TEntity> query = Query(where, orderBy, includes, skipPage, takePage, skip, take, tracking);
            return query.Select(select).FirstOrDefault();
        }

        public virtual TEntity FindLast(
            Expression<Func<TEntity, bool>> where = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includes = null,
            int? skipPage = null,
            int? takePage = null,
            int? skip = null,
            int? take = null,
            bool tracking = false)
        {
            IQueryable<TEntity> query = Query(where, orderBy, includes, skipPage, takePage, skip, take, tracking);
            return query.LastOrDefault();
        }
        public virtual TResult FindLast<TResult>(
            Expression<Func<TEntity, TResult>> select,
            Expression<Func<TEntity, bool>> where = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includes = null,
            int? skipPage = null,
            int? takePage = null,
            int? skip = null,
            int? take = null,
            bool tracking = false)
        {
            IQueryable<TEntity> query = Query(where, orderBy, includes, skipPage, takePage, skip, take, tracking);
            return query.Select(select).LastOrDefault();
        }

        public virtual TEntity Insert(TEntity entity)
        {
            return Add(entity).Entity;
        }

        public virtual void InsertRange(IEnumerable<TEntity> entities)
        {
            AddRange(entities);
        }

        public new TEntity Update(TEntity entity)
        {
            return base.Update(entity).Entity;
        }

        public virtual void Delete(object id)
        {
            var entity = Find(id);
            Delete(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            Remove(entity);
        }

        public virtual IQueryable<TEntity> Queryable()
        {
            return this;
        }

        protected IQueryable<TEntity> Query(
            Expression<Func<TEntity, bool>> where = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includes = null,
            int? skipPage = null,
            int? takePage = null,
            int? skip = null,
            int? take = null,
            bool tracking = false)
        {
            IQueryable<TEntity> query = this;

            if (!tracking)
            {
                query = query.AsNoTracking();
            }
            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (skipPage != null && takePage != null)
            {
                query = query.Skip((skipPage.Value - 1) * takePage.Value).Take(takePage.Value);
            }
            if (skip != null)
            {
                query = query.Skip(skip.Value - 1);
            }
            if (take != null)
            {
                query = query.Take(take.Value);
            }
            if (where != null)
            {
                query = query.Where(where);
            }
            return query;
        }

        protected IQueryable<TEntity> BuildFindPredicate(object key, bool activateTracking = false)
        {
            var parameter = Expression.Parameter(typeof(TEntity), "x");

            if (activateTracking)
            {
                return this.Where((Expression<Func<TEntity, bool>>)
                Expression.Lambda(
                    Expression.Equal(
                        Expression.Property(parameter, "Id"),
                        Expression.Constant(key)),
                    parameter));
            }

            return this.AsNoTracking().Where((Expression<Func<TEntity, bool>>)
                Expression.Lambda(
                    Expression.Equal(
                        Expression.Property(parameter, "Id"),
                        Expression.Constant(key)),
                    parameter));
        }
    }
}