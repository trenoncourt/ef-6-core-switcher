using System;
using System.Data;
using Microsoft.EntityFrameworkCore;
using EfSwitcher.DataContext.Abstractions;

namespace EfSwitcher.DataContext.EfCore
{
    public class DataContext : DbContext, IDataContext
    {
        protected IDbTransaction Transaction;

        public DataContext()
        {
            InstanceId = Guid.NewGuid();
        }

        public DataContext(DbContextOptions options) : base(options)
        {
            InstanceId = Guid.NewGuid();
        }

        public Guid InstanceId { get; }


        public virtual void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
            IDbConnection connection = Database.GetDbConnection();
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            Transaction = connection.BeginTransaction(isolationLevel);
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