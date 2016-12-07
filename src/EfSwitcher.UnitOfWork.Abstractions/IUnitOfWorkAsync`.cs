namespace EfSwitcher.UnitOfWork.Abstractions
{
    public interface IUnitOfWorkAsync<TDbContext> : IUnitOfWork<TDbContext>, IUnitOfWorkAsync
        where TDbContext : IUnitOfWorkAsync
    {
    }
}