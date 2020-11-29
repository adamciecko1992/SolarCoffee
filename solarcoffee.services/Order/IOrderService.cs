using System.Collections.Generic;
using solarcoffee.data.models;

namespace solarcoffee.services.Order
{
    public interface IOrderService
    {
        List<SalesOrder> GetOrders();
        ServiceResponse<bool> GenerateOpenOrder(SalesOrder order);
        ServiceResponse<SalesOrder> MarkFullfilled(int id);
    }
}
