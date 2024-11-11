https://learn.microsoft.com/fr-fr/dotnet/core/extensions/dependency-injection-usage
https://www.c-sharpcorner.com/article/using-dependency-injection-in-net-console-apps/
https://github.com/csandun/ConsoleAppWithDI/tree/master

https://dev.to/krusty93/net-core-5-0-console-app-configuration-trick-and-tips-c1j


https://stackoverflow.com/questions/9218847/how-do-i-handle-database-connections-with-dapper-in-net
https://www.learndapper.com/parameters

https://stackoverflow.com/questions/38878140/how-can-i-implement-dbcontext-connection-string-in-net-core


https://www.c-sharpcorner.com/article/using-dependency-injection-in-net-console-apps/


//The AgencyContext class would look like this:

using Microsoft.EntityFrameworkCore;

namespace CallCenter.Entities
{
    public class AgencyContext : DbContext
    {
        public AgencyContext(string connectionString) : base(GetOptions(connectionString))
        {
            
        }

        private static DbContextOptions GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        }

        public DbSet<Companys> Companys { get; set; }
    }
}

//The startup.c IServiceProvider module has this:

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
           
            services.AddDbContext<InsuranceContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.UseRowNumberForPaging()));
            services.AddScoped<ITestRepository , TestRepository >();
....
}
And finally the appsettings.js



https://blog.kritner.com/2018/09/14/dotnet-core-console-application-ioptions-t-configuration/
