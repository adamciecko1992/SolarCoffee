using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarcoffee.data.models
{
   public class ProductInventorySnapshot
    {
        public int Id { get; set; }
        public DateTime SnapshotTime { get; set; }
        public int  QuantityOnHande { get; set; }
        public Product Product { get; set; }
   
    }
}
