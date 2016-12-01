using System.Threading;
using System.Threading.Tasks;

namespace EfSwitcher.DataContext.Abstractions
{
    public interface IDataContextAsync : IDataContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}