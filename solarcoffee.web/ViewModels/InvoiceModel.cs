using System;
using System.Collections.Generic;

namespace solarcoffee.web.ViewModels
{
    public class InvoiceModel
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int CustomerId { get; set; }
        public List<SalesItemOrderModel> LineItems { get; set; }
    }
    public class SalesItemOrderModel
    {
        public int Id { get; set; }
        public ProductModel Product { get; set; }
        public int Quantity { get; set; }
    }
}
