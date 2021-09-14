using PK.OrdersWatcher.Shared.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using PK.OrdersWatcher.Shared.Extensions;

namespace PK.OrdersWatcher.Shared.Services
{
    public class CsvToXmlService : ICsvToXmlService
    {
        public string ToXml(IEnumerable<Order> orders)
        {
            var xws = new XmlWriterSettings();
            xws.OmitXmlDeclaration = true;
            xws.Indent = true;

            using (var sw = new StringWriter())
            {
                using (var xw = XmlWriter.Create(sw, xws))
                {
                    xw.WriteStartElement("Orders");

                    foreach (var order in orders.ToList())
                    {
                        var orderChild = new XElement("Order",
                            new XElement("OrderNo", order.OrderNo),
                            new XElement("ConsignmentNo", order.ConsignmentNo),
                            new XElement("ParcelCode", order.ParcelCode),
                            new XElement("ConsignmentName", order.ConsigneeName),

                            new XElement("Address",
                                new XElement("Address1", order.Address.Address1),
                                new XElement("Address2", order.Address.Address2),
                                new XElement("City", (!string.IsNullOrEmpty(order.Address.City)
                                    ? order.Address.City
                                    : string.Empty)),
                                new XElement("State", (!string.IsNullOrEmpty(order.Address.State))
                                    ? order.Address.State
                                    : string.Empty)),
                            new XElement("Country", (!string.IsNullOrEmpty(order.Address.CountryCode)
                                ? order.Address.CountryCode
                                : string.Empty)),
                            new XElement("Description", order.ItemDescription),
                            new XElement("ItemQuantity", order.ItemQuantity),
                            new XElement("ItemValue", order.ItemValue),
                            new XElement("ItemWeight", order.ItemWeight),
                            new XElement("Currency", (!string.IsNullOrEmpty(order.ItemCurrency)
                                ? order.ItemCurrency
                                : string.Empty))
                        );
                        orderChild.WriteTo(xw);
                        
                    }

                    var summary = orders.ToList().OrdersSummary();
                    var summElement = new XElement("OrdersSummary",
                        new XElement("ItemsCount", orders.Count()),
                        new XElement("TotalWeight", summary.Weight),
                        new XElement("ItemQuantity", summary.Quantity),
                        new XElement("Total", summary.Total)
                    );
                    summElement.WriteTo(xw);

                    xw.WriteEndElement();
                }

                return sw.ToString();
            }
        }
    }
}
