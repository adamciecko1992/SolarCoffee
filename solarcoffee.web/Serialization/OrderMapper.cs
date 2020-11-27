using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using solarcoffee.data.models;
using solarcoffee.web.ViewModels;

namespace solarcoffee.web.Serialization
{/// <summary>
/// Serialize order data model to one thats important for view
/// </summary>
    public static class OrderMapper
    {
        public static SalesOrder SerializeInvoiceToOrder( InvoiceModel invoice)
        {
            var SalesOrderItems = invoice.LineItems
                .Select(item => new SalesOrderItem
                {
                    Id = item.Id,
                    Quantity = item.Quantity,
                    Product = ProductMapper.SerializeProductModel(item.Product)
                }).ToList() ;
            return new SalesOrder
            {
                SolarOrderedItems = SalesOrderItems,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
                Id = invoice.Id,
            };

        }

        public static List<OrderModel> SerializeOrderToViewModels(IEnumerable<SalesOrder> orders )
        {
            return orders
                .Select((order) => new OrderModel
                {
                    Id = order.Id,
                    SolarOrderedItems = SerializeSalesOrderModel(order.SolarOrderedItems),
                    CreatedOn = order.CreatedOn,
                    UpdatedOn = order.UpdatedOn,
                    Customer = CustomerMapper.SerializeCustomer(order.Customer),
                    IsPayed = order.IsPaid


                }).ToList();
        }
        private static List<SalesItemOrderModel> SerializeSalesOrderModel(IEnumerable<SalesOrderItem> orders)
        {
            return orders.Select((order) => new SalesItemOrderModel
            {
                Id = order.Id,
                Quantity = order.Quantity,
                Product = ProductMapper.SerializeProductModel(order.Product),
            }).ToList();
        }

        
    }
}
