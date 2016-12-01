using System;
using Microsoft.EntityFrameworkCore;
using EfSwitcher.DataContext.Abstractions;

namespace EfSwitcher.DataContext.EfCore
{
    public class DataContext : DbContext, IDataContextAsync
    {
        public DataContext()
        {
            InstanceId = Guid.NewGuid();
        }

        public DataContext(DbContextOptions options) : base(options)
        {
            InstanceId = Guid.NewGuid();
        }

        public Guid InstanceId { get; }
    }
}