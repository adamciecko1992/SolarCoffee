using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        //Musze stowrzyc tabele Customers, ktora bedzie zawierac Customerow
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerAdress> CustomerAdress { get; set; }

    }
}
