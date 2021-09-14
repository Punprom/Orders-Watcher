using Moq;
using PK.OrdersWatcher.Shared.Dtos;
using PK.OrdersWatcher.Shared.Extensions;
using PK.OrdersWatcher.Shared.Models;
using PK.OrdersWatcher.Shared.Services;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using Xunit;


namespace PK.OrdersWatcher.Tests
{
    public class CsvLoadingTests
    {
        private const string _dataFile =  @"..\..\..\csv\csvdata.csv";

        private IReadOnlyList<string> _contentsList; 
        private string _headersLine;
        private IReadOnlyList<string> _headers;

        private Mock<ICsvService> _mockCsvService;
        private Mock<ICsvToXmlService> _mockCsvToXmlService;

        public CsvLoadingTests()
        {
            SettingUp();
        }

        [Fact]
        public void Test_csvservice_reading_orders_from_csv_file()
        {
            var csvService = _mockCsvService.Object;
            var readLines = csvService.GetOrdersFromCsvFile(_dataFile, ',');
            
            Assert.NotEmpty(readLines);

        }

        [Xunit.Theory]
        [InlineData(4,10)]
        [InlineData(0, 2)]
        [InlineData(3,5)]
        [InlineData(3,3)]
        public void Test_getting_order_data_object_from_csv_line(int line, int col)
        {
            var delimiter = ',';
            var dataLine = _contentsList.Skip(1).ToList()[line];
            var dto = new OrderDto();
            dto.ReadCsvLine(dataLine, delimiter);

            var actualItems = new List<string>();
            var lineItems = dataLine.Split(delimiter).ToList();
            lineItems.ForEach(item => actualItems.Add(item.Trim()));

            var expectedValue = string.Empty;
            var actualValue = actualItems[col].Trim();
            switch (col)
            {
                case 2:
                    expectedValue = dto.ParcelCode;
                    break;
                case 5:
                    expectedValue = dto.Address2;
                    break;
                case 10:
                    expectedValue = dto.ItemValue.ToString();
                    break;
                case 3:
                    expectedValue = dto.ConsigneeName;
                    break;

            } 
            
            Assert.Equal(actualValue, expectedValue);
        }

        [Xunit.Theory]
        [InlineData(2, "10 Baker Street", "Peckham", "London")]
        [InlineData(4, "Casa di Bolos", "Dulce Av.", "Milano")]
        public void Test_Order_data_objects_list_to_order_models(int line, string expectedAddress1, string expectedAddress2,
            string expectedCity)
        {
            var csvService = _mockCsvService.Object;
            var dtosList = csvService.GetOrdersFromCsvFile(_dataFile, ',')
                .OrderBy(o => o.OrderNo)
                .ToList();
            Xunit.Assert.NotEmpty(dtosList);

            var models = dtosList.ToModels();
            var expectedModel = models.ToList()[line];

            Assert.Equal(expectedModel.Address.Address1, expectedAddress1);
            Assert.Equal(expectedModel.Address.Address2, expectedAddress2);
            Assert.Equal(expectedModel.Address.City, expectedCity);
        }


        [Fact]
        public void Test_getting_orders_data_objects_to_xml_string()
        {
            var delimiter = ',';
            var csvService = _mockCsvService.Object;
            var dtos = csvService.GetOrdersFromCsvFile(_dataFile, delimiter);
            var orders = dtos.ToList().ToModels();
            Assert.NotEmpty(orders);
            
            _mockCsvToXmlService.Setup(x => x.ToXml(It.IsAny<IEnumerable<Order>>()))
                .Returns(() =>
                {
                    var service = new CsvToXmlService();
                    var xml = service.ToXml(orders);

                    return xml;
                });
            var xmlService = _mockCsvToXmlService.Object;
            var xml = xmlService.ToXml(orders);
            Assert.NotEmpty(xml);

            var xDoc = XDocument.Parse(xml);
            var isNodeTypeDoc = (xDoc.NodeType == XmlNodeType.Document);
           
            Assert.True(isNodeTypeDoc);
            
        }

        private void SettingUp()
        {
            var lines = File.ReadAllLines(_dataFile);
            _contentsList = lines.ToList().AsReadOnly();
            _headersLine = _contentsList.First();

            var headers = new List<string>();
            var headings = _headersLine.Split(',');
            headings.ToList().ForEach(item => headers.Add(item.Trim()));
            _headers = headings.ToList().AsReadOnly();

            _mockCsvService = new Mock<ICsvService>();
            _mockCsvService.Setup(x => x.GetOrdersFromCsvFile(It.IsAny<string>(), It.IsAny<char>()))
                .Returns(() =>
                {
                    var service = new CsvService();
                    var list = service.GetOrdersFromCsvFile(_dataFile, ',');

                    return list.AsEnumerable();
                });

            _mockCsvToXmlService = new Mock<ICsvToXmlService>();
           
        }
    }
}
