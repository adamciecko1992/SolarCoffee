using solarcoffee.data.models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace solarcoffee.data
{
    public class solarDbContext :IdentityDbContext
    {
     
        public solarDbContext() { }
        public solarDbContext(DbContextOptions options): base(options)
        {
           
        }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerAdress> CustomerAdress { get; set; }
        public virtual DbSet<ProductInventory> ProductInventories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductInventorySnapshot> ProductInventorySnapshots { get; set; }
        public virtual DbSet<SalesOrder> SalesOrders { get; set; }
        public virtual DbSet<SalesOrderItem> SalesOrdersItems { get; set; }


    }
}
