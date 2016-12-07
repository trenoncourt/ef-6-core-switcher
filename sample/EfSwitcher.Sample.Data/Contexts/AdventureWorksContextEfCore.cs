using EfSwitcher.Sample.Data.Entities;
using EfSwitcher.UnitOfWork.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace EfSwitcher.Sample.Data.Contexts
{
    public partial class AdventureWorksContextEfCore : DataContext.EfCore.DataContextAsync, IUnitOfWork<AdventureWorksContextEfCore>
    {
        public AdventureWorksContextEfCore(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<BuildVersion> BuildVersion { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<CustomerAddress> CustomerAddress { get; set; }
        public virtual DbSet<ErrorLog> ErrorLog { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductCategory> ProductCategory { get; set; }
        public virtual DbSet<ProductDescription> ProductDescription { get; set; }
        public virtual DbSet<ProductModel> ProductModel { get; set; }
        public virtual DbSet<ProductModelProductDescription> ProductModelProductDescription { get; set; }
        public virtual DbSet<SalesOrderDetail> SalesOrderDetail { get; set; }
        public virtual DbSet<SalesOrderHeader> SalesOrderHeader { get; set; }
    }
}