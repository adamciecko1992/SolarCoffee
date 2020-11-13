using solarcoffee.data.models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using solarcoffee.data;
using Microsoft.Extensions.Logging;

namespace solarcoffee.services.Inventory
{
    class InventoryService : IInventoryService
    {
        private readonly solarDbContext _db;
        private readonly ILogger<InventoryService> _logger;


        public InventoryService(solarDbContext dbContext, ILogger<InventoryService> logger)
        {
            _db = dbContext;
            _logger = logger;
        }
       
        public ProductInventory GetByPdouctId(int productId)
        {
            return _db.ProductInventories
                .Include(inv=>inv.Product)
                .FirstOrDefault(inv=>inv.Id==productId);
        }

        public List<ProductInventory> GetCurrentInventory()
        {
            return _db.ProductInventories
                .Include(pi => pi.Product)
                .Where(pi => !pi.IsArchived)
                .ToList();
        }

        public List<ProductInventorySnapshot> GetSnapshotHistory()
        {
            var earliest = DateTime.UtcNow - TimeSpan.FromHours(6);
            //mozna odejmowac czas w ten sposob ! 
            return _db.ProductInventorySnapshots
                .Include(inv=>inv.Product)
                .Where(snap=>snap.SnapshotTime > earliest && !snap.Product.IsArchived)
                .ToList();
        }
        /// <summary>
        /// Update number of units available of the provided product Id
        /// Adjust quantity on hand by adjustment value
        /// </summary>
        /// <param name="id">ProductId</param>
        /// <param name="adjustment">Number of units added/removed from inventory</param>
        /// <returns></returns>
        public ServiceResponse<ProductInventory> UpdateUnitAvailable(int id, int adjustment)
        {
            try {
                //lazy loaded so need to include models in query
                var inventory = _db.ProductInventories
                        .Include(inventory => inventory.Product)
                        .First(inventory => inventory.Id == id);
                inventory.QuantityOnHand=adjustment;
                try {
                    CreateSnapshot(inventory);
                } catch(Exception e) {
                    _logger.LogError("Error creating inventory snapshot");
                    _logger.LogError(e.Message);
                };

                _db.SaveChanges();

                return new ServiceResponse<ProductInventory>
                {
                    IsSuccess = true,
                    Data = inventory,
                    Message = $"Quantity of {id} changed successfully by {adjustment}",
                    Time = DateTime.UtcNow
                };
            
            
            }catch {
                return new ServiceResponse<ProductInventory>
                {
                    IsSuccess = true,
                    Data = null,
                    Message ="Error on updateing product quantity at hand",
                    Time = DateTime.UtcNow
                };
            };

        }
        private void CreateSnapshot(ProductInventory inventory)
        {
            var snapshot = new ProductInventorySnapshot
            {
                Id = inventory.Id,
                Product = inventory.Product,
                QuantityOnHande = inventory.QuantityOnHand,
                SnapshotTime = DateTime.UtcNow
            };
            //_db.ProductInventorySnapshots.Add(snapshot);
            _db.Add(snapshot); //tez zadziala bo entity zna typ danych wiec wrzuci je do prawidlowej tabeli

        }
    }
}
