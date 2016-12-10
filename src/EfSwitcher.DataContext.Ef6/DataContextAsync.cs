using EfSwitcher.Repository.Abstractions;
using EfSwitcher.Repository.Ef6;
using EfSwitcher.UnitOfWork.Abstractions;

namespace EfSwitcher.DataContext.Ef6
{
    public class DataContextAsync : DataContext, IUnitOfWorkAsync
    {
        public DataContextAsync(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }

        public IRepositoryAsync<TEntity> RepositoryAsync<TEntity>() where TEntity : class
        {
            return new RepositoryAsync<TEntity>(Set<TEntity>());
        }
    }
}