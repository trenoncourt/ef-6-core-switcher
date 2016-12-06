using EfSwitcher.DataContext.Abstractions;
using System;
using System.Data;

namespace EfSwitcher.UnitOfWork.Abstractions
{
    public interface IUnitOfWork<TDataContext> : IDisposable where TDataContext : IDataContext
    {
        int SaveChanges();
        void Dispose(bool disposing);
        void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified);
        void Commit();
        void Rollback();
    }
}