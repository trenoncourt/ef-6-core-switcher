using System.Threading;
using System.Threading.Tasks;
using EfSwitcher.Repository.Abstractions;

namespace EfSwitcher.UnitOfWork.Abstractions
{
    public interface IUnitOfWorkAsync : IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        IRepositoryAsync<TEntity> RepositoryAsync<TEntity>() where TEntity : class;
    }
}