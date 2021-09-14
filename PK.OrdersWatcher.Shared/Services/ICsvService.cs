using System.Collections.Generic;
using System.Threading.Tasks;
using PK.OrdersWatcher.Shared.Dtos;

namespace PK.OrdersWatcher.Shared.Services
{
    public interface ICsvService
    {
        IEnumerable<OrderDto> GetOrdersFromCsvFile(string filePath, char seperator);

    }
}