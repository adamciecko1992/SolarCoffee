using System;

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
  
        public string AdressLine1 { get; set; }
    
        public string AdressLine2 { get; set; }
    
        public string State { get; set; }
       
        public string PostalCode { get; set; }
    }
}

