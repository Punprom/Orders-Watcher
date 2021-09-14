using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PK.OrdersWatcher.Shared.Models;
using Xunit;

namespace PK.OrdersWatcher.Tests
{
    public class OrderHeaderTests
    {
        private const string _dataFile =  @"..\..\..\csv\csvdata.csv";

        private IReadOnlyList<string> _contentsList; 
        private readonly string _headers;

        public OrderHeaderTests()
        {
            var lines = File.ReadAllLines(_dataFile);
            _contentsList = lines.ToList().AsReadOnly();
            _headers = _contentsList.First();
        }

        [Theory]
        [InlineData("Order No")]
        [InlineData("City")]
        [InlineData("Country Code")]
        [InlineData("Consignee Name")]
        public void Test_check_see_if_column_index_coresponding_to_index_of_header_enumerations(string arg)
        {
            var headers = new List<string>();
            var headings = _headers.Split(',').ToList();
            headings.ForEach(item => {headers.Add(item.Trim());});
            var expectedItemIndex = headers.FindIndex(s => s.Equals(arg));

            int actualHeaderIndex = -1;
            switch (arg)
            {
                case "Order No":
                    actualHeaderIndex = (int)CsvHeader.OrderNo;
                    break;
                case "City":
                    actualHeaderIndex = (int)CsvHeader.City;
                    break;
                case "Country Code":
                    actualHeaderIndex = (int)CsvHeader.CountryCode;
                    break;
                case "Consignee Name":
                    actualHeaderIndex = (int) CsvHeader.ConsigneeName;
                    break;
                    
            }
            
            Assert.Equal(expectedItemIndex, actualHeaderIndex);
        }

    }
}
