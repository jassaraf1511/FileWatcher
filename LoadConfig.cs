using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Binder;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using Microsoft.Extensions.Configuration.FileExtensions;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration.UserSecrets;
using Cfsb.Incoming.FedWires.DataEntities;


namespace Cfsb.Incoming.FedWires.Services
{
    public  class LoadConfig
    {

        private   ApplicationSettings appSettings;
        private    IConfigurationRoot configuration;

        public  ApplicationSettings AppSettings { get => appSettings; set => appSettings = value; }
        public IConfigurationRoot Configuration { get => configuration; set => configuration = value; }

        public ApplicationSettings StartApplication(string appSettingFile=null)
        {

          

            var builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                     // .AddUserSecrets<Program>()
              .AddEnvironmentVariables();
            
            configuration = builder.Build();
           

       
            appSettings = new ApplicationSettings();
           

        configuration.GetSection("ApplicationSettings").Bind(appSettings);
            return appSettings;
        }

        private static T GetOptions<T>(IConfiguration configuration)
         where T : new()
        {
            var options = new T();
            configuration.Bind(options);
            return options;
        }
    }
}
