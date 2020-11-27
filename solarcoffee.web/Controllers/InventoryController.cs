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
        private readonly IInventoryService _inventoryService;
        private ILogger<InventoryController> _logger;

        public InventoryController(ILogger<InventoryController> logger, IInventoryService inventoryservice)
        {
            _inventoryService = inventoryservice;
            _logger = logger;
        }

        [HttpGet("/api/inventory")]
        public ActionResult GetInvenotry()
        {
            _logger.LogInformation("Getting Inventory");
            var inventory = _inventoryService.GetCurrentInventory()
                 .Select(pi => new ProductInventoryModel
                 {
                     Id = pi.Id,
                     Product = ProductMapper.SerializeProductModel(pi.Product),
                     IdealQuantity = pi.IdealQuantity,
                     QuantityOnHand = pi.QuantityOnHand
                 })
                 .OrderBy(inv => inv.Product.Name)
                 .ToList();

            return Ok(inventory);
        }

        [HttpPatch("/api/inventory")]
        public ActionResult UpdateInventory([FromBody] ShipmentModel shipment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _logger.LogInformation("Inventory is getting updated");
            var id = shipment.IdProduct;
            var adjustment = shipment.ProductAdjustment;
            var inventory = _inventoryService.UpdateUnitsAvailable(id, adjustment);
            return Ok(inventory);
        }

    }
}
