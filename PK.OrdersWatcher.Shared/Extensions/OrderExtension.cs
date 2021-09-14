using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PK.OrdersWatcher.Shared.Dtos;
using PK.OrdersWatcher.Shared.Models;

namespace PK.OrdersWatcher.Shared.Extensions
{
    public static class OrderExtension
    {
        public static OrderDto ReadCsvLine(this OrderDto dto, string line, char seperator)
        {
            var fields = line.Split(seperator);

            dto.OrderNo = fields[CsvHeader.OrderNo];
            dto.ConsignmentNo = fields[CsvHeader.ConsignmentNo];
            dto.ParcelCode = fields[CsvHeader.ParcelCode];
            dto.ConsigneeName = fields[CsvHeader.ConsigneeName];
            dto.Address1 = fields[CsvHeader.Address1];
            dto.Address2 = fields[CsvHeader.Address2];
            dto.City = fields[CsvHeader.City];
            dto.State = fields[CsvHeader.State];
            dto.CountryCode = fields[CsvHeader.CountryCode];
            dto.ItemQuantity = Convert.ToInt32(fields[CsvHeader.ItemQuantity]);
            dto.ItemValue = Convert.ToDecimal(fields[CsvHeader.ItemValue]);
            dto.ItemWeight = Convert.ToDecimal(fields[CsvHeader.ItemWeight]);
            dto.ItemDescription = fields[CsvHeader.ItemDescription];
            dto.ItemCurrency = fields[CsvHeader.ItemCurrency];

            return dto;
        }

        /// <summary>
        /// Convert data objects to order models
        /// </summary>
        /// <param name="orders">order data object extension</param>
        /// <returns>order model arrays</returns>
        public static IEnumerable<Order> ToModels(this List<OrderDto> orders)
        {
            var list = new List<Order>();
            foreach (var dto  in orders)
            {
                var model = new Order();
                var address = new Address()
                {
                    Address1 = dto.Address1,
                    Address2 = dto.Address2,
                    City = dto.City,
                    State = dto.State,
                    CountryCode = dto.CountryCode
                };

                model.OrderNo = dto.OrderNo;
                model.ConsignmentNo = dto.ConsignmentNo;
                model.ParcelCode = dto.ParcelCode;
                model.ConsigneeName = dto.ConsigneeName;
                model.ItemQuantity = dto.ItemQuantity;
                model.ItemValue = dto.ItemValue;
                model.ItemWeight = dto.ItemWeight;
                model.ItemDescription = dto.ItemDescription;
                model.ItemCurrency = dto.ItemCurrency;
                model.Address = address;

                list.Add(model);
            }

            return list.AsEnumerable();
        }

        /// <summary>
        /// get orders summary
        /// </summary>
        /// <param name="orders">orders models <see cref="Order"/></param>
        /// <returns>order summary object <see cref="OrdersSummary"/></returns>
        public static OrderSummary OrdersSummary(this List<Order> orders)
        {
            var sum = 0M;
            var qty = 0M;
            var weight = 0M;

            orders.ForEach(order =>
            {
                qty += order.ItemQuantity;
                weight += order.ItemWeight;
                sum += order.ItemValue;
            });
            var summary = new OrderSummary() { Quantity = qty, Weight = weight, Total = sum };

            return summary;
        }
    }
}
