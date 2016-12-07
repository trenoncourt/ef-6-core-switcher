using System.Data.Entity;
using EfSwitcher.Sample.Data.Entities;
using EfSwitcher.UnitOfWork.Abstractions;

namespace EfSwitcher.Sample.Data.Contexts
{
    public partial class AdventureWorksContextEf6 : DataContext.Ef6.DataContextAsync, IUnitOfWorkAsync<AdventureWorksContextEf6>
    {
        public AdventureWorksContextEf6(string nameOrConnectionString) : base(nameOrConnectionString)
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