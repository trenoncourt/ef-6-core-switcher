using System.Threading;
using System.Threading.Tasks;
using EfSwitcher.DataContext.Abstractions;

namespace EfSwitcher.UnitOfWork.Abstractions
{
    public interface IUnitOfWorkAsync<TDataContext> : IUnitOfWork<TDataContext> where TDataContext : IDataContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}