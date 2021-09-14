using PK.OrdersWatcher.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PK.OrdersWatcher.Shared.Extensions;

namespace PK.OrdersWatcher.Shared.Services
{
    /// <summary>
    /// CSVService - csv service class
    /// </summary>
    public class CsvService : ICsvService
    {
        /// <summary>
        /// get order data objects array from csv file
        /// </summary>
        /// <param name="filePath">full csv file path</param>
        /// <param name="seperator">comma limited separator sign</param>
        /// <returns>order data objects <see cref="OrderDto"/></returns>
        public IEnumerable<OrderDto> GetOrdersFromCsvFile(string filePath, char seperator)
        {
            var list = new List<OrderDto>();
            var rawContents = File.ReadAllText(filePath);
            var csvLines = rawContents.Split(Environment.NewLine).ToList();
            
            var header = csvLines.Take(1);
            var lines = csvLines.Skip(1).Take(csvLines.Count - 2).ToList();
            lines.ForEach(line =>
            {
                list.Add(new OrderDto().ReadCsvLine(line,seperator));
            });

            return list.AsEnumerable();
        }
    }
}
