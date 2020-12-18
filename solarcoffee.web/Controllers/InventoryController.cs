using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using solarcoffee.services.Inventory;
using solarcoffee.web.ViewModels;
using AutoMapper;

namespace solarcoffee.web.Controllers
{
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;
        private ILogger<InventoryController> _logger;
        private IMapper _mapper;

        public InventoryController(ILogger<InventoryController> logger, IInventoryService inventoryservice, IMapper mapper)
        {
            _inventoryService = inventoryservice;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("/api/inventory")]
        public ActionResult GetInvenotry()
        {
            _logger.LogInformation("Getting Inventory");
            var wholeInventory = _inventoryService.GetCurrentInventory();
            var inventoryViewData = wholeInventory.Select((inv) => _mapper.Map<ProductInventoryModel>(inv))
                .OrderBy(inv => inv.Product.Name)
                .ToList();

            return Ok(inventoryViewData);
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

        [HttpGet("/api/inventory/snapshots")]
        public ActionResult GetSnapshotHistory()
        {

     
            _logger.LogInformation("Getting snapshot history");

            try
            {
                var snapshotHistory = _inventoryService.GetSnapshotHistory();

              
                var timelineMarkers = snapshotHistory
                    .Select(t => t.SnapshotTime)
                    .Distinct()
                    .ToList();

              
                var snapshots = snapshotHistory
                    .GroupBy(hist => hist.Product, hist => hist.QuantityOnHand,
                        (key, g) => new ProductInventorySnapshotModel
                        {
                            ProductId = key.Id,
                            QuantityOnHand = g.ToList()
                        })
                    .OrderBy(hist => hist.ProductId)
                    .ToList();

                var viewModel = new SnapshotResponse
                {
                    Timeline = timelineMarkers,
                    ProductInventorySnapshots = snapshots
                };

                return Ok(viewModel);
            }
            catch (Exception e)
            {
                _logger.LogError("Error getting snapshot history.");
                _logger.LogError(e.StackTrace);
                return BadRequest("Error retrieving history");
            }
        }

    }
}
