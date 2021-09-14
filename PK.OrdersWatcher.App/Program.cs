using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PK.OrdersWatcher.App.Helpers;
using PK.OrdersWatcher.Shared.Services;

namespace PK.OrdersWatcher.App
{
    class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        public static IServiceProvider ServiceProvider { get; } 


        static void Main(string[] args)
        {
            Watching();
        }

        static void Watching()
        {
            var watchPath = Configuration["WatchPath"];
            var fileSysWatcher = new FileSystemWatcher(watchPath);
            fileSysWatcher.EnableRaisingEvents = true;

            fileSysWatcher.Created += new FileSystemEventHandler(OnFileDropIn);

            Console.WriteLine($"Watching directory: {watchPath} press a key to stop.");
            Console.ReadLine();

            
        }

        static void OnFileDropIn(object sender, FileSystemEventArgs e)
        {
            var csvFile = $"orders_{DateTime.Now.ToString("dd-MM-yyyy")}.csv";
            var fileToCheck = Path.GetFileName(e.Name);
            
            if (e.Name.Equals(csvFile, StringComparison.CurrentCultureIgnoreCase))
            {
                // getting IoC services collection provider & build
                var serviceProvider = new ServiceCollection()
                    .AddSingleton<ICsvService, CsvService>()
                    .AddSingleton<ICsvToXmlService, CsvToXmlService>()
                    .BuildServiceProvider();
               
               DoProcssingCsvFile(e.FullPath, serviceProvider);
            }
        }

        static void DoProcssingCsvFile(string csvFullPath, ServiceProvider service)
        {
            var csvService = service.GetService<ICsvService>();
            var xmlService = service.GetService<ICsvToXmlService>();
            var delimiter = char.Parse(Configuration["CsvSeperator"]);
            var outputPath = Configuration["XmlOutputPath"];
            
            var csvProcessHelper = new OrderCsvToXmlProcessHelper(csvService, xmlService);
            var orders = csvProcessHelper.ProcessCsvFile(csvFullPath, delimiter);
            var xml = csvProcessHelper.ConvertToXml(orders.ToList());
            csvProcessHelper.WriteToFile(xml, outputPath);

            Console.WriteLine($"Xml generated path: {outputPath}");
            Console.WriteLine("All done");
        }

        
    }
}
