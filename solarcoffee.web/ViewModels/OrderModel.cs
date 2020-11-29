using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace solarcoffee.web.ViewModels
{
    public class OrderModel
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public CustomerModel Customer { get; set; }
        public List<SalesItemOrderModel> SolarOrderedItems { get; set; }
        public bool IsPaid { get; set; }
    }
}
