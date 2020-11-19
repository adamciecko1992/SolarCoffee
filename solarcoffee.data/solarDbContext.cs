using solarcoffee.data.models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace solarcoffee.data
{
    public class solarDbContext :IdentityDbContext
    {
        //reprezenting db that we wanna target
        public solarDbContext() { }
        //dbcontext options opisuja dokaldnie jaka baze danych bedziemy konsumowac,
        public solarDbContext(DbContextOptions options): base(options)
        {
           
        }
        //Musze stowrzyc tabele Customers i wszystkie inne wymienione, ktora bedzie zawierac Customerow zgodnych z modelem
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerAdress> CustomerAdress { get; set; }
        public virtual DbSet<ProductInventory> ProductInventories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductInventorySnapshot> ProductInventorySnapshots { get; set; }
        public virtual DbSet<SalesOrder> SalesOrders { get; set; }
        public virtual DbSet<SalesOrderItem> SalesOrdersItems { get; set; }


    }
}
