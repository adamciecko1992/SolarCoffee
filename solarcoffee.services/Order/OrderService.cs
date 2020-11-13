using solarcoffee.data.models;
using System;
using System.Linq;
using System.Collections.Generic;
using solarcoffee.data;
using Microsoft.EntityFrameworkCore;
using solarcoffee.services.Product;
using solarcoffee.services.Inventory;

namespace solarcoffee.services.Order
{
    class OrderService : IOrderService
    {
        private readonly solarDbContext _db;
        private readonly IProductService _productService;
        private readonly IInventoryService _inventoryService;


        public OrderService(
            solarDbContext dbContext,
            IProductService productService,
            IInventoryService inventoryService
            )
        {
            _db = dbContext;
            _inventoryService = inventoryService;
            _productService = productService;
        }

        public ServiceResponse<bool> GenerateInvoiceForOrder(SalesOrder order)
        {
           foreach(var item  in order.SolarOrderedItems)
            {
                item.Product = _productService.GetProductById(item.Product.Id);
                item.Quantity = item.Quantity;
                var inventoryId = _inventoryService.GetByPdouctId(item.Product.Id).Id;
                _inventoryService.UpdateUnitAvailable(inventoryId, -item.Quantity);

            }
        }

        public List<SalesOrder> GetOrders()
        {
            return _db.SalesOrders
                .Include(order => order.Customer)
                    .ThenInclude(customer=>customer.PrimaryAdress)
                .Include(order => order.SolarOrderedItems)
                    .ThenInclude(order=>order.Product)
                .ToList();
        }

        public ServiceResponse<bool> MarkFullfilled(int id)
        {
            throw new NotImplementedException();
        }
    }
}
