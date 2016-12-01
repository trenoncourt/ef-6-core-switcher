using System;

namespace EfSwitcher.DataContext.Abstractions
{
    public interface IDataContext : IDisposable
    {
        int SaveChanges();
    }
}