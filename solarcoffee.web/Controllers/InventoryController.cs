using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using solarcoffee.services.Inventory;
using solarcoffee.web.ViewModels;
using solarcoffee.web.Serialization;


namespace solarcoffee.web.Controllers
{
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventory;
        private ILogger<InventoryController> _logger;

        public InventoryController(ILogger<InventoryController> logger, IInventoryService inventoryservice)
        {
            _inventory = inventoryservice;
            _logger = logger;
        }

        [HttpGet("/api/inventory")]
        public ActionResult GetInvenotry()
        {
            _logger.LogInformation("Getting Inventory");
            var inventory = _inventory.GetCurrentInventory()
                .Select((inventoryItem) => new ProductInventoryModel
                {
                   Id = inventoryItem.Id,
                   IdealQuantity = inventoryItem.IdealQuantity,
                   IsArchived = inventoryItem.IsArchived,
                   QuantityOnHand = inventoryItem.QuantityOnHand,
                   Product =ProductMapper.SerializeProductModel( inventoryItem.Product), 
                
                }).OrderBy((inv) => inv.Product.Name)
                .ToList();

            return Ok(inventory);
        }

        [HttpPatch("/api/inventory")]
        public ActionResult UpdateInventory([FromBody] ShipmentModel shipment)
        {
            _logger.LogInformation("Inventory is getting updated");
            var id = shipment.IdProduct;
            var adjustment = shipment.ProductAdjustment;
            var inventory = _inventory.UpdateUnitAvailable(id, adjustment);
            return Ok(inventory);
        }

    }
}
