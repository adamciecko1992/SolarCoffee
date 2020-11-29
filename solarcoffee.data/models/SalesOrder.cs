using System;
using System.Collections.Generic;

namespace solarcoffee.data.models
{
    public class SalesOrder
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public Customer Customer { get; set; }
        public List<SalesOrderItem> LineItems {get;set;}
        public bool IsPaid { get; set; }
    }
}
