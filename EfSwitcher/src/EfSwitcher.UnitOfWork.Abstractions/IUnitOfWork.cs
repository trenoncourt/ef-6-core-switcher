using System;
using System.Data;
using EfSwitcher.Repository.Abstractions;

namespace EfSwitcher.UnitOfWork.Abstractions
{
    public interface IUnitOfWork : IDisposable
    {
        int SaveChanges();
        void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified);
        void Commit();
        void Rollback();
        IRepository<TEntity> Repository<TEntity>() where TEntity : class;
    }
}