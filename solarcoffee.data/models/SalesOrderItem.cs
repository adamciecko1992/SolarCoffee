﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarcoffee.data.models
{
   public  class SalesOrderItem
    {
        public int Id { get; set; }
     
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
