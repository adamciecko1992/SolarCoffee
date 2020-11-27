using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarcoffee.data.models
{
  public class SalesOrder
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public Customer Customer { get; set; }
        public List<SalesOrderItem> SolarOrderedItems {get;set;}
        public bool IsPaid { get; set; }
    }
}
