using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace solarcoffee.web.ViewModels
{
    public class CustomerModel
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public CustomerAdressModel PrimaryAdress { get; set; }

     
    }

    public class CustomerAdressModel
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        [MaxLength(35)] 
        public string AdressLine1 { get; set; }
        [MaxLength(35)]
        public string AdressLine2 { get; set; }
        [MaxLength(25)]
        public string State { get; set; }
        [MaxLength(10)]
        public string PostalCode { get; set; }
    }
}

