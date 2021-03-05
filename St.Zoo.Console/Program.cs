using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using St.Zoo.Business;
using St.Zoo.Data;
using St.Zoo.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace St.Zoo.Console
{
    class Program
    {
        /// <summary>
        /// The prices file path.
        /// </summary>
        private const string Prices_File_Path = "prices.txt";
        /// <summary>
        /// The animals file path.
        /// </summary>
        private const string Animals_File_Path = "zoo.xml";

        /// <summary>
        /// The species file path.
        /// </summary>
        private const string Species_File_Path = "animals.csv";

        private static ILogger _logger;

        static void Main(string[] args)
        {
            try
            {
                var serviceProvider = InitializeContainer();

                _logger.LogInformation("Application initialized.");

                var service = serviceProvider.GetService<IZooService>();
                var amount = service.GetTotalFoodPrice();

                System.Console.WriteLine($"Total Amount: {amount}");
            }
            catch (System.Exception ex)
            {
#if DEBUG
                _logger.LogError(ex.ToString());
#else
                _logger.LogError(ex.Message);
#endif
            }

            System.Console.WriteLine("Press any key to continue...");
            System.Console.ReadKey();
        }

        /// <summary>
        /// Initialize container
        /// </summary>
        /// <returns></returns>
        public static IServiceProvider InitializeContainer()
        {

            // Nuget: Microsoft.Extensions.DependencyInjection
            // Optional Logging: Microsoft.Extensions.Logging (.Console)
            // Optional Configuration: Microsoft.Extensions.Configuration (.Json, .Binder)
            // Optional Options: Microsoft.Extensions.Options (.ConfigurationExtensions)

            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", true, true)
               .Build();

            var provider = new PhysicalFileProvider(Directory.GetCurrentDirectory());

            var serviceProvider = new ServiceCollection()
                .AddLogging(configure => configure.AddConsole())
                .AddSingleton(configuration)
                .AddOptions()                
                .AddScoped<IZooService, ZooService>()
                .AddScoped<IFoodRepository, FoodRepository>(_ => new FoodRepository(provider.GetFileInfo(configuration["AppStore:PricesFilePath"])))
                .AddScoped<IAnimalSpecieRepository, AnimalSpecieRepository>(_ => new AnimalSpecieRepository(provider.GetFileInfo(configuration["AppStore:SpeciesFilePath"])))
                .AddScoped<IAnimalRepository, AnimalRepository>(_ => new AnimalRepository(provider.GetFileInfo(configuration["AppStore:AnimalsFilePath"])))
                .Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Debug)
                .Configure<AppProfile>(configuration.GetSection("AppProfile"))
                .Configure<AppSettings>(configuration.GetSection("AppSettings"))
                .Configure<AppStore>(configuration.GetSection("AppStore"))
                .BuildServiceProvider();

            _logger = serviceProvider
                     .GetService<ILoggerFactory>()
                     .CreateLogger<Program>();


            //var builder = new ConfigurationBuilder();

            //var dic = new Dictionary<string, string>
            //{
            //    {"AppProfile:Label", "Default"}
            //};
            //builder.AddInMemoryCollection(dic);
            //var memoryConfiguration = builder.Build();

            return serviceProvider;
        }
    }
}