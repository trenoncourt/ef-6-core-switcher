using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using EfSwitcher.Repository.Abstractions;
using System.Data.Entity;

namespace EfSwitcher.Repository.Ef6
{
    public abstract class RepositoryAsync<TEntity> : Repository<TEntity>, IRepositoryAsync<TEntity> 
        where TEntity : class
    {
        public virtual async Task<IEnumerable<TEntity>> SelectAsync(
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
            return await query.ToListAsync().ConfigureAwait(false);
        }

        public virtual async Task<IEnumerable<TResult>> SelectAsync<TResult>(
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
            return await query.Select(select).ToListAsync().ConfigureAwait(false);
        }

        public virtual async Task<TEntity> FindFirstAsync(
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
            return await query.FirstOrDefaultAsync().ConfigureAwait(false);
        }

        public virtual async Task<TResult> FindFirstAsync<TResult>(
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
            return await query.Select(select).FirstOrDefaultAsync().ConfigureAwait(false);
        }

        public virtual async Task<TEntity> FindLastAsync(
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
            return await query.OrderByDescending(q => q).FirstOrDefaultAsync().ConfigureAwait(false);
        }

        public virtual async Task<TResult> FindLastAsync<TResult>(
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
            return await query.Select(select).OrderByDescending(q => q).FirstOrDefaultAsync().ConfigureAwait(false);
        }
        
        public virtual async Task<bool> DeleteAsync(object key)
        {
            return await DeleteAsync(CancellationToken.None, key).ConfigureAwait(false);
        }

        public virtual async Task<bool> DeleteAsync(CancellationToken cancellationToken, object key)
        {
            var entity = await FindAsync(cancellationToken, key);
            if (entity == null)
            {
                return false;
            }
            Remove(entity);
            return true;
        }

        public async Task<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken)
        {
            return await base.FindAsync(cancellationToken, keyValues);
        }
    }
}