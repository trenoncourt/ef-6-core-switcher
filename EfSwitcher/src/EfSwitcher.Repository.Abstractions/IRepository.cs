using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EfSwitcher.Repository.Abstractions
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> Select(
            Expression<Func<TEntity, bool>> where = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includes = null,
            int? skipPage = null,
            int? takePage = null,
            int? skip = null,
            int? take = null,
            bool tracking = false);

        IEnumerable<TResult> Select<TResult>(
            Expression<Func<TEntity, TResult>> select,
            Expression<Func<TEntity, bool>> where = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includes = null,
            int? skipPage = null,
            int? takePage = null,
            int? skip = null,
            int? take = null,
            bool tracking = false);

        TEntity Find(params object[] keyValues);
        
        TEntity FindFirst(
            Expression<Func<TEntity, bool>> where = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includes = null,
            int? skipPage = null,
            int? takePage = null,
            int? skip = null,
            int? take = null,
            bool tracking = false);

        TResult FindFirst<TResult>(
            Expression<Func<TEntity, TResult>> select,
            Expression<Func<TEntity, bool>> where = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includes = null,
            int? skipPage = null,
            int? takePage = null,
            int? skip = null,
            int? take = null,
            bool tracking = false);

        TEntity FindLast(
            Expression<Func<TEntity, bool>> where = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includes = null,
            int? skipPage = null,
            int? takePage = null,
            int? skip = null,
            int? take = null,
            bool tracking = false);

        TResult FindLast<TResult>(
            Expression<Func<TEntity, TResult>> select,
            Expression<Func<TEntity, bool>> where = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includes = null,
            int? skipPage = null,
            int? takePage = null,
            int? skip = null,
            int? take = null,
            bool tracking = false);

        TEntity Insert(TEntity entity);

        void InsertRange(IEnumerable<TEntity> entities);

        TEntity Update(TEntity entity);

        void Delete(object id);

        void Delete(TEntity entity);

        IQueryable<TEntity> Queryable();
    }
}