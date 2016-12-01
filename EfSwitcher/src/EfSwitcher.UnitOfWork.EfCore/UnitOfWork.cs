using System;
using System.Data;
using System.Data.Common;
using EfSwitcher.DataContext.Abstractions;
using EfSwitcher.UnitOfWork.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace EfSwitcher.UnitOfWork.EfCore
{
    public class UnitOfWork<TDataContext> : IUnitOfWork<TDataContext> where TDataContext : IDataContext
    {
        protected bool Disposed;
        protected IDataContext DataContext;
        protected DbConnection Connection;
        protected DbTransaction Transaction;

        public UnitOfWork(TDataContext dataContext)
        {
            DataContext = dataContext;
        }

        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (Disposed)
                return;

            if (disposing)
            {
                try
                {
                    if (Connection != null && Connection.State == ConnectionState.Open)
                    {
                        Connection.Close();
                    }
                }
                catch (ObjectDisposedException) {}

                if (DataContext != null)
                {
                    DataContext.Dispose();
                    DataContext = null;
                }
            }

            Disposed = true;
        }
        

        public virtual int SaveChanges()
        {
            return DataContext.SaveChanges();
        }
        

        public virtual void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
            Connection = ((DbContext)DataContext).Database.GetDbConnection();

            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }

            Transaction = Connection.BeginTransaction(isolationLevel);
        }

        public virtual bool Commit()
        {
            Transaction.Commit();
            return true;
        }

        public virtual void Rollback()
        {
            Transaction.Rollback();
        }
    }
}