using System;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using EfSwitcher.Repository.Abstractions;
using EfSwitcher.Repository.Ef6;
using EfSwitcher.UnitOfWork.Abstractions;

namespace EfSwitcher.DataContext.Ef6
{
    public class DataContext : DbContext, IUnitOfWork
    {
        protected DbTransaction Transaction;

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

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            return Set<TEntity>() as Repository<TEntity>;
        }
    }
}