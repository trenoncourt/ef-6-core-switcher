//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Threading;
//using System.Threading.Tasks;

//namespace EfSwitcher.Repository.Ef6.Extensions
//{
//    public static class QueryableExtension
//    {
//        public static TEntity FindFirst<TEntity>(
//            this IQueryable<TEntity> query,
//            Expression<Func<TEntity, bool>> where = null,
//            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
//            List<Expression<Func<TEntity, object>>> includes = null,
//            int? skipPage = null,
//            int? takePage = null,
//            int? skip = null,
//            int? take = null,
//            bool tracking = false) where TEntity : class
//        {
//            query = query.Build(where, orderBy, includes, skipPage, takePage, skip, take, tracking);
//            return query.FirstOrDefault();
//        }

//        public static TResult FindFirst<TEntity, TResult>(
//            this IQueryable<TEntity> query,
//            Expression<Func<TEntity, TResult>> select,
//            Expression<Func<TEntity, bool>> where = null,
//            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
//            List<Expression<Func<TEntity, object>>> includes = null,
//            int? skipPage = null,
//            int? takePage = null,
//            int? skip = null,
//            int? take = null,
//            bool tracking = false) where TEntity : class
//        {
//            query = query.Build(where, orderBy, includes, skipPage, takePage, skip, take, tracking);
//            return query.Select(select).FirstOrDefault();
//        }

//        public static TEntity FindLast<TEntity>(
//            this IQueryable<TEntity> query,
//            Expression<Func<TEntity, bool>> where = null,
//            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
//            List<Expression<Func<TEntity, object>>> includes = null,
//            int? skipPage = null,
//            int? takePage = null,
//            int? skip = null,
//            int? take = null,
//            bool tracking = false) where TEntity : class
//        {
//            query = query.Build(where, orderBy, includes, skipPage, takePage, skip, take, tracking);
//            return query.OrderByDescending(q => q).FirstOrDefault();
//        }
//        public static TResult FindLast<TEntity, TResult>(
//            this IQueryable<TEntity> query,
//            Expression<Func<TEntity, TResult>> select,
//            Expression<Func<TEntity, bool>> where = null,
//            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
//            List<Expression<Func<TEntity, object>>> includes = null,
//            int? skipPage = null,
//            int? takePage = null,
//            int? skip = null,
//            int? take = null,
//            bool tracking = false) where TEntity : class
//        {
//            query = query.Build(where, orderBy, includes, skipPage, takePage, skip, take, tracking);
//            return query.Select(select).OrderByDescending(q => q).FirstOrDefault();
//        }
        
//        private static IQueryable<TEntity> Build<TEntity>(
//            this IQueryable<TEntity> query,
//            Expression<Func<TEntity, bool>> where = null,
//            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
//            List<Expression<Func<TEntity, object>>> includes = null,
//            int? skipPage = null,
//            int? takePage = null,
//            int? skip = null,
//            int? take = null,
//            bool tracking = false) where TEntity : class
//        {
//            if (!tracking)
//            {
//                query = query.AsNoTracking();
//            }
//            if (includes != null)
//            {
//                query = includes.Aggregate(query, (current, include) => current.Include(include));
//            }
//            if (orderBy != null)
//            {
//                query = orderBy(query);
//            }
//            if (skipPage != null && takePage != null)
//            {
//                query = query.Skip((skipPage.Value - 1) * takePage.Value).Take(takePage.Value);
//            }
//            if (skip != null)
//            {
//                query = query.Skip(skip.Value - 1);
//            }
//            if (take != null)
//            {
//                query = query.Take(take.Value);
//            }
//            if (where != null)
//            {
//                query = query.Where(where);
//            }
//            return query;
//        }

//        public static async Task<TEntity> FindFirstAsync<TEntity>(
//            this IQueryable<TEntity> query,
//            Expression<Func<TEntity, bool>> where = null,
//            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
//            List<Expression<Func<TEntity, object>>> includes = null,
//            int? skipPage = null,
//            int? takePage = null,
//            int? skip = null,
//            int? take = null,
//            bool tracking = false) where TEntity : class
//        {
//            query = query.Build(where, orderBy, includes, skipPage, takePage, skip, take, tracking);
//            return await query.FirstOrDefaultAsync().ConfigureAwait(false);
//        }

//        public static async Task<TResult> FindFirstAsync<TEntity, TResult>(
//            this IQueryable<TEntity> query,
//            Expression<Func<TEntity, TResult>> select,
//            Expression<Func<TEntity, bool>> where = null,
//            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
//            List<Expression<Func<TEntity, object>>> includes = null,
//            int? skipPage = null,
//            int? takePage = null,
//            int? skip = null,
//            int? take = null,
//            bool tracking = false) where TEntity : class
//        {
//            query = query.Build(where, orderBy, includes, skipPage, takePage, skip, take, tracking);
//            return await query.Select(select).FirstOrDefaultAsync().ConfigureAwait(false);
//        }

//        public static async Task<TEntity> FindLastAsync<TEntity>(
//            this IQueryable<TEntity> query,
//            Expression<Func<TEntity, bool>> where = null,
//            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
//            List<Expression<Func<TEntity, object>>> includes = null,
//            int? skipPage = null,
//            int? takePage = null,
//            int? skip = null,
//            int? take = null,
//            bool tracking = false) where TEntity : class
//        {
//            query = query.Build(where, orderBy, includes, skipPage, takePage, skip, take, tracking);
//            return await query.OrderByDescending(q => q).FirstOrDefaultAsync().ConfigureAwait(false);
//        }

//        public static async Task<TResult> FindLastAsync<TEntity, TResult>(
//            this IQueryable<TEntity> query,
//            Expression<Func<TEntity, TResult>> select,
//            Expression<Func<TEntity, bool>> where = null,
//            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
//            List<Expression<Func<TEntity, object>>> includes = null,
//            int? skipPage = null,
//            int? takePage = null,
//            int? skip = null,
//            int? take = null,
//            bool tracking = false) where TEntity : class
//        {
//            query = query.Build(where, orderBy, includes, skipPage, takePage, skip, take, tracking);
//            return await query.Select(select).OrderByDescending(q => q).FirstOrDefaultAsync().ConfigureAwait(false);
//        }
//        public static async Task<TEntity> FindAsync<TEntity>(this IQueryable<TEntity> query, object[] keyValues, CancellationToken cancellationToken) where TEntity : class
//        {
//            return await ((DbSet<TEntity>)query).FindAsync(cancellationToken, keyValues);
//        }

//        public static async Task<TEntity> FindAsync<TEntity>(this IQueryable<TEntity> query, object[] keyValues)
//        {
//            return await query.FindAsync(keyValues);
//        }

//        public static async Task<bool> DeleteAsync<TEntity>(this IQueryable<TEntity> query, object key)
//        {
//            return await DeleteAsync(CancellationToken.None, key).ConfigureAwait(false);
//        }

//        public virtual async Task<bool> DeleteAsync<TEntity>(this IQueryable<TEntity> query, CancellationToken cancellationToken, object key)
//        {
//            var entity = await query.Find.FindAsync(cancellationToken, key);
//            if (entity == null)
//            {
//                return false;
//            }
//            _dbSet.Remove(entity);
//            return true;
//        }
//    }
//}