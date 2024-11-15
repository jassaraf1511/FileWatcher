
 ITag result = (from farmer in message.Block4
               where farmer.TagName.Substring(0, 2) == "20"
               where farmer.Qualifier == "SEME"
               select farmer).First();
Console.WriteLine(result.Value);
https://dotnettutorials.net/lesson/first-and-firstordefault-methods-in-linq/

//Dapper
https://learn.microsoft.com/en-us/ef/core/dbcontext-configuration/
https://www.c-sharpcorner.com/article/api-development-using-dapper-and-microsoft-asp-net-core-web-api/ (good
https://stackoverflow.com/questions/43058497/dependency-injection-with-netcore-for-dal-and-connection-string
https://www.c-sharpcorner.com/article/using-dapper-for-data-access-in-asp-net-core-applications/
https://stackoverflow.com/questions/69472240/asp-net-6-identity-sqlite-services-adddbcontext-how
https://reintech.io/blog/integrating-dapper-dependency-injection-dotnet-core

https://learn.microsoft.com/fr-fr/dotnet/core/extensions/dependency-injection-usage
https://www.c-sharpcorner.com/article/using-dependency-injection-in-net-console-apps/
https://github.com/csandun/ConsoleAppWithDI/tree/master

https://dev.to/krusty93/net-core-5-0-console-app-configuration-trick-and-tips-c1j


https://stackoverflow.com/questions/9218847/how-do-i-handle-database-connections-with-dapper-in-net
https://www.learndapper.com/parameters

https://stackoverflow.com/questions/38878140/how-can-i-implement-dbcontext-connection-string-in-net-core






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


delegate List<string> StoredProcedureParameterProvider(IDbConnection connection, string storedProcedureName)

SqlMapper.StoredProcedureParameterProvider = (connection, storedProcedureName) =>
    // full implementation would want to cache this based upon the name
    connection.Query<string> (
        @"SELECT PARAMETER_NAME
            FROM INFORMATION_SCHEMA.PARAMETERS
            WHERE SPECIFIC_SCHEMA = PARSENAME(@storedProcedureName, 2)
            AND SPECIFIC_NAME = PARSENAME(@storedProcedureName, 1)",
        buffered: false,
        param: {storedProcedureName})
        .Select(name => name.TrimStart('@')
        .ToList ();
		
		https://github.com/DapperLib/Dapper/issues/393
		
		
		https://www.oreilly.com/library/view/adonet-cookbook/0596004397/ch04s10.html#:~:text=DeriveParameters(%20)%20method,-The%20first%20technique&text=The%20name%20of%20the%20stored,procedure%20into%20a%20Parameters%20collection.
		
		
select  
   'Parameter_name' = name,  
   'Type'   = type_name(user_type_id),  
   'Length'   = max_length,  
   'Prec'   = case when type_name(system_type_id) = 'uniqueidentifier' 
              then precision  
              else OdbcPrec(system_type_id, max_length, precision) end,  
   'Scale'   = OdbcScale(system_type_id, scale),  
   'Param_order'  = parameter_id,  
   'Collation'   = convert(sysname, 
                   case when system_type_id in (35, 99, 167, 175, 231, 239)  
                   then ServerProperty('collation') end)  

  from sys.parameters where object_id = object_id('sp_insert_Action_Option_Cash_Payout')
  
  select  *
  

  from sys.parameters where object_id = object_id('sp_insert_Action_Option_Cash_Payout')
  
  
  https://www.google.com/search?q=sql+server+get+stored+procedure+parameter+list&sca_esv=04e68c913ef2a454&sxsrf=ADLYWIKTYN5ex5TRYwLPr1It9n2TnKcUqg%3A1731649685939&ei=leA2Z8D5OMCj5NoP97yB-Qs&oq=sql+server+retrieve+stored+procedure+parameters+&gs_lp=Egxnd3Mtd2l6LXNlcnAiMHNxbCBzZXJ2ZXIgcmV0cmlldmUgc3RvcmVkIHByb2NlZHVyZSBwYXJhbWV0ZXJzICoCCAIyBhAAGBYYHjIGEAAYFhgeMgYQABgWGB4yBhAAGBYYHjIGEAAYFhgeMgYQABgWGB4yBhAAGBYYHjIIEAAYgAQYogQyCBAAGIAEGKIEMggQABiABBiiBEjIPVAAWP4acAB4AZABAJgBowGgAfAIqgEEMTIuMbgBAcgBAPgBAZgCDaACpQnCAgQQIxgnwgIIEAAYFhgKGB7CAgsQABiABBiGAxiKBcICCBAAGKIEGIkFmAMA4gMFEgExIECSBwQxMS4yoAfShAE&sclient=gws-wiz-serp
  
  
  
  USE AdventureWorks;
GO

SELECT 
   SCHEMA_NAME(SCHEMA_ID) AS [Schema]
  ,SO.name AS [ObjectName]			   
  ,SO.Type_Desc AS [ObjectType (UDF/SP)]
  ,P.parameter_id AS [ParameterID]
  ,P.name AS [ParameterName]
  ,TYPE_NAME(P.user_type_id) AS [ParameterDataType]
  ,P.max_length AS [ParameterMaxBytes]
  ,P.is_output AS [IsOutPutParameter]
FROM sys.objects AS SO
INNER JOIN sys.parameters AS P ON SO.OBJECT_ID = P.OBJECT_ID
ORDER BY [Schema], SO.name, P.parameter_id
GO


SELECT 
   SCHEMA_NAME(SCHEMA_ID) AS [Schema]
  ,SO.name AS [ObjectName]			   
  ,SO.Type_Desc AS [ObjectType (UDF/SP)]
  ,P.parameter_id AS [ParameterID]
  ,P.name AS [ParameterName]
  ,TYPE_NAME(P.user_type_id) AS [ParameterDataType]
  ,P.max_length AS [ParameterMaxBytes]
  ,P.is_output AS [IsOutPutParameter]
FROM sys.objects AS SO
INNER JOIN sys.parameters AS P ON SO.OBJECT_ID = P.OBJECT_ID
where SO.name='sp_insert_Swift_Message'
ORDER BY [Schema], SO.name, P.parameter_id
GO
