using System;
using System.Data.Entity;
using EfSwitcher.DataContext.Abstractions;

namespace EfSwitcher.DataContext.Ef6
{
    public class DataContext : DbContext, IDataContextAsync
    {
        public DataContext()
        {
            InstanceId = Guid.NewGuid();
        }

        public DataContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            InstanceId = Guid.NewGuid();
        }

        public Guid InstanceId { get; }
    }
}