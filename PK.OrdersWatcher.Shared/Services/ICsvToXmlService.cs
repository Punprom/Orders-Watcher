using System.Collections.Generic;
using PK.OrdersWatcher.Shared.Models;

namespace PK.OrdersWatcher.Shared.Services
{
    /// <summary>
    /// ICsvToXmlService - Csv to Xml service contract interface
    /// </summary>
    public interface ICsvToXmlService
    {
        /// <summary>
        /// To xml string based
        /// </summary>
        /// <param name="orders">order model arrays <see cref="Order"/></param>
        /// <returns>Xml string based file</returns>
        string ToXml(IEnumerable<Order> orders);
    }
}