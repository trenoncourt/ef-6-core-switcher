using EfSwitcher.Sample.Data.Contexts;
using EfSwitcher.UnitOfWork.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EfSwitcher.Sample.Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWorkAsync<AdventureWorksContextEf6>, AdventureWorksContextEf6>(provider => new AdventureWorksContextEf6("connectionString"));
            services.AddScoped<IUnitOfWork<AdventureWorksContextEfCore>, AdventureWorksContextEfCore>(provider =>
                new AdventureWorksContextEfCore(new DbContextOptionsBuilder().UseSqlServer("connectionString").Options));
            services.AddMvc();
        }
        
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            app.UseMvc();
        }
    }
}
