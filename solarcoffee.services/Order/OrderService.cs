﻿using solarcoffee.data.models;
using System;
using System.Linq;
using System.Collections.Generic;
using solarcoffee.data;
using Microsoft.EntityFrameworkCore;
using solarcoffee.services.Product;
using solarcoffee.services.Inventory;


namespace solarcoffee.services.Order
{
   public class OrderService : IOrderService
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

     
        public ServiceResponse<bool> GenerateOpenOrder(SalesOrder order)
        {
           foreach(var item  in order.LineItems)
            {
                item.Product = _productService.GetProductById(item.Product.Id);

                var inventoryId = _inventoryService.GetByProductId(item.Product.Id);
                _inventoryService.UpdateUnitsAvailable(inventoryId.Id, -item.Quantity);

            }
           
                _db.SalesOrders.Add(order);
                _db.SaveChanges();
                return new ServiceResponse<bool>
                {
                    Data = true,
                    IsSuccess = true,
                    Time = DateTime.UtcNow,
                    Message = "Successfully generated invoice"
                };
                 
                
          
              
        }

        public List<SalesOrder> GetOrders()
        {
            return _db.SalesOrders
                .Include(order => order.Customer)
                    .ThenInclude(customer=>customer.PrimaryAdress)
                .Include(order => order.LineItems)
                    .ThenInclude(order=>order.Product)
                .ToList();
        }

        public ServiceResponse<SalesOrder> MarkFullfilled(int id)
        {
            var order = _db.SalesOrders.Find(id);
            order.UpdatedOn = DateTime.UtcNow;
            order.IsPaid = true;


                _db.SalesOrders.Update(order);
                _db.SaveChanges();
                return new ServiceResponse<SalesOrder>
                {
                    IsSuccess = true,
                    Data = order,
                    Message = "Order closed, invoice paid in full",
                    Time = DateTime.UtcNow
                };

            
           
        }
    }
}
