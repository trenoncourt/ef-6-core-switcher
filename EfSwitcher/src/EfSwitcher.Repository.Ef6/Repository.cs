using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using EfSwitcher.DataContext.Abstractions;
using EfSwitcher.Repository.Abstractions;

namespace EfSwitcher.Repository.Ef6
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly IDataContext DataContext;

        public Repository(IDataContext context)
        {
            DataContext = context;
            var dbContext = context as DbContext;
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }
            DbSet = dbContext.Set<TEntity>();
        }

        protected readonly DbSet<TEntity> DbSet;

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

        public virtual TEntity Find(params object[] key)
        {
            return DbSet.Find(key);
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
            ((DbContext) DataContext).Entry(entity).State = EntityState.Added;
            return DbSet.Attach(entity);
        }

        public virtual void InsertRange(IEnumerable<TEntity> entities)
        {
            DbSet.AddRange(entities);
        }

        public virtual TEntity Update(TEntity entity)
        {
            ((DbContext)DataContext).Entry(entity).State = EntityState.Added;
            return DbSet.Attach(entity);
        }

        public virtual void Delete(object id)
        {
            var entity = Find(id);
            Delete(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        public virtual IQueryable<TEntity> Queryable()
        {
            return DbSet;
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
            IQueryable<TEntity> query = DbSet;

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
                return DbSet.Where((Expression<Func<TEntity, bool>>)
                Expression.Lambda(
                    Expression.Equal(
                        Expression.Property(parameter, "Id"),
                        Expression.Constant(key)),
                    parameter));
            }

            return DbSet.AsNoTracking().Where((Expression<Func<TEntity, bool>>)
                Expression.Lambda(
                    Expression.Equal(
                        Expression.Property(parameter, "Id"),
                        Expression.Constant(key)),
                    parameter));
        }
    }
}