using PK.OrdersWatcher.Shared.Extensions;
using PK.OrdersWatcher.Shared.Models;
using PK.OrdersWatcher.Shared.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace PK.OrdersWatcher.App.Helpers
{
    /// <summary>
    /// OrderCsvToXmlProcessHelper - orders csv to xml processing helper
    /// </summary>
    public class OrderCsvToXmlProcessHelper
    {
        private readonly ICsvService _csvService;
        private readonly ICsvToXmlService _csvToXmlService;
       
        public OrderCsvToXmlProcessHelper(ICsvService csvService,
            ICsvToXmlService csvToXmlService)
        {
            _csvService = csvService ?? throw new ArgumentNullException(nameof(csvService));
            _csvToXmlService = csvToXmlService ?? throw new ArgumentNullException(nameof(csvToXmlService));

        }

        public IEnumerable<Order> ProcessCsvFile(string csvFilePath, char delimiter)
        {
            Console.WriteLine($"Processing file: {csvFilePath}");
            
            Console.WriteLine("Reading file and converting to data objects...");
            var orderDtos = _csvService.GetOrdersFromCsvFile(csvFilePath, delimiter);
            Thread.Sleep(200);

            Console.WriteLine("Processing to Xml file...");
            var models = orderDtos.ToList().ToModels();

            return models;
        }

        public string ConvertToXml(List<Order> orders)
        {
            var xml = _csvToXmlService.ToXml(orders);

            return xml;
        }

        public void WriteToFile(string xml, string outputPath)
        {
            if (!Directory.Exists(outputPath))
                Directory.CreateDirectory(outputPath);

            var outputFile = $"xml-orders_{DateTime.Now.ToString("dd-MM-yyyy")}.xml";
            var destPath = Path.Combine(outputPath, outputFile);
            File.WriteAllText(destPath, xml);
        }
    }
}
