using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using solarcoffee.data.models;
namespace solarcoffee.services.Inventory
{
    interface IInventoryService
    {
        public List<ProductInventory> GetCurrentInventory();
        public ServiceResponse<ProductInventory> UpdateUnitAvailable(int id, int adjustment);
        public ProductInventory GetByPdouctId(int productId);
        public List<ProductInventorySnapshot> GetSnapshotHistory();

    }
}
