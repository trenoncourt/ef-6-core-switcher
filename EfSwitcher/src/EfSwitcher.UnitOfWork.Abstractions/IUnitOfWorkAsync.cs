using System.Threading;
using System.Threading.Tasks;
using EfSwitcher.DataContext.Abstractions;

namespace EfSwitcher.UnitOfWork.Abstractions
{
    public interface IUnitOfWorkAsync<TDataContextAsync> : IUnitOfWork<TDataContextAsync> where TDataContextAsync : IDataContextAsync
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}