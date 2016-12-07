namespace EfSwitcher.UnitOfWork.Abstractions
{
    public interface IUnitOfWork<TDbContext> : IUnitOfWork
        where TDbContext : IUnitOfWork
    {
    }
}