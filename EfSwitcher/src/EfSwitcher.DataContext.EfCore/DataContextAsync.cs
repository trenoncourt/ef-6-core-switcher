using EfSwitcher.Repository.Abstractions;
using EfSwitcher.Repository.EfCore;
using EfSwitcher.UnitOfWork.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace EfSwitcher.DataContext.EfCore
{
    public class DataContextAsync : DataContext, IUnitOfWorkAsync
    {
        public DataContextAsync(DbContextOptions options) : base(options)
        {
        }

        public IRepositoryAsync<TEntity> RepositoryAsync<TEntity>() where TEntity : class
        {
            return Set<TEntity>() as RepositoryAsync<TEntity>;
        }
    }
}