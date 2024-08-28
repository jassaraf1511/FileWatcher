using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Serilog;
using Serilog.Enrichers;
using Serilog.Events;


namespace CFSB.FileWatcherService
{
    public class Program
    {
        public static IConfiguration Configuration { get; set; }
        public static void Main(string[] args)
        {
            AppSettings appSettings = new AppSettings();

            Configuration = new ConfigurationBuilder()
            .AddJsonFile("appSettings.json")
            .AddEnvironmentVariables()
            .Build();

            Configuration.GetSection("AppSettings").Bind(appSettings);

            string loggerTemplate = @"{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u4}]<{ThreadId}> [{SourceContext:l}] {Message:lj}{NewLine}{Exception}";
            var baseDir = appSettings.LogDirectory;
            //loggerTemplate = appSettings.LoggerTemplate;
            var logfile = Path.Combine(baseDir, appSettings.LogFileName + ".log");
            //var logfile = Path.Combine(@"C:\CFSB\LOG", "CFSB_FileWatcher" + ".log");
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.With(new ThreadIdEnricher())
                .Enrich.FromLogContext()
                 .WriteTo.Console(LogEventLevel.Information, loggerTemplate)
                .WriteTo.File(logfile, LogEventLevel.Information, loggerTemplate,
                    rollingInterval: RollingInterval.Day, retainedFileCountLimit: 90)
                .CreateLogger();
            Log.Information("====================================================================");
            Log.Information($"Application Starts. Version: {System.Reflection.Assembly.GetEntryAssembly()?.GetName().Version}");
            Log.Information($"Application info: {appSettings.InputFolder}");

                       
            CreateHostBuilder(args).Build().Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)

                .ConfigureServices((hostContext, services) =>
                {

                    services.AddHostedService<FileWatchWorker>();

                    services.Configure<AppSettings>(hostContext.Configuration.GetSection("AppSettings"));

                })
           .UseWindowsService()
           .UseSerilog();

    }
}

