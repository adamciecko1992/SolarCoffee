using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using solarcoffee.data;
using solarcoffee.data.models;


namespace solarcoffee.services.Inventory
{

    public class InventoryService : IInventoryService
    {
        private readonly solarDbContext _db;
        private readonly ILogger<InventoryService> _logger;

        public InventoryService(solarDbContext dbContext, ILogger<InventoryService> logger)
        {
            _db = dbContext;
            _logger = logger;
        }

   
        public List<ProductInventory> GetCurrentInventory()
        {
            return _db.ProductInventories
                .Include(pi => pi.Product)
                .Where(pi => !pi.Product.IsArchived)
                .ToList();
        }

       
        public ServiceResponse<ProductInventory> UpdateUnitsAvailable(int id, int adjustment)
        {

                var now = DateTime.UtcNow;
               
                var inventory = _db.ProductInventories
                    .Include(inv => inv.Product)
                    .First(inv => inv.Product.Id == id);

                inventory.QuantityOnHand += adjustment;
            try
            {
                CreateSnapshot();
            }

            catch (Exception e)
            {
                _logger.LogError("Error creating inventory snapshot.");
                _logger.LogError(e.StackTrace);
            }
            _db.SaveChanges();

                return new ServiceResponse<ProductInventory>

                {
                    IsSuccess = true,
                    Data = inventory,
                    Message = $"Product {id} inventory adjusted",
                    Time = now
                };
           

            
        }

      
        public ProductInventory GetByProductId(int productId)
        {
            return _db.ProductInventories
                .Include(pi => pi.Product)
                .FirstOrDefault(pi => pi.Product.Id == productId);
        }

        public List<ProductInventorySnapshot> GetSnapshotHistory()
        {
            var earliest = DateTime.UtcNow - TimeSpan.FromHours(2);

            return _db.ProductInventorySnapshots
                .Include(snap => snap.Product)
                .Where(snap
                    => snap.SnapshotTime > earliest
                       && !snap.Product.IsArchived)
                .ToList();
        }

        private void CreateSnapshot()
        {
            var now = DateTime.UtcNow;

            var inventories = _db.ProductInventories
                .Include(inv => inv.Product)
                .ToList();

            foreach (var inventory in inventories)
            {
                var snapshot = new ProductInventorySnapshot
                {
                    SnapshotTime = now,
                    Product = inventory.Product,
                    QuantityOnHand = inventory.QuantityOnHand
                };

                _db.Add(snapshot);
            }
        }
    }
}