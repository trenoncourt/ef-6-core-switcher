using System;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using EfSwitcher.DataContext.Abstractions;

namespace EfSwitcher.DataContext.Ef6
{
    public class DataContext : DbContext, IDataContext
    {
        protected DbTransaction Transaction;
        public DataContext()
        {
            InstanceId = Guid.NewGuid();
        }

        public DataContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            InstanceId = Guid.NewGuid();
        }

        public Guid InstanceId { get; }

        public virtual void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
            ObjectContext objectContext = ((IObjectContextAdapter)this).ObjectContext;
            if (objectContext.Connection.State != ConnectionState.Open)
            {
                objectContext.Connection.Open();
            }
            Transaction = objectContext.Connection.BeginTransaction(isolationLevel);
        }

        public virtual void Commit()
        {
            Transaction.Commit();
        }

        public virtual void Rollback()
        {
            Transaction.Rollback();
        }
    }
}