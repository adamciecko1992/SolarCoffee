using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using solarcoffee.data.models;
using solarcoffee.web.ViewModels;

namespace solarcoffee.web.Serialization
{
    public static class CustomerMapper
    {
        /// <summary>
        /// Serializes a Customer data model into a CustomerModel view model
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public static CustomerModel SerializeCustomer(Customer customer)
        {
            var adress = new CustomerAdressModel
            {
                Id = customer.Id,
                CreatedOn = customer.CreatedOn,
                UpdatedOn = customer.UpdatedOn,
               AdressLine1 =customer.PrimaryAdress.AdressLine1,
                 
               AdressLine2 = customer.PrimaryAdress.AdressLine2,
               PostalCode = customer.PrimaryAdress.PostalCode,
               State = customer.PrimaryAdress.State,
            };


            return new CustomerModel
            {
                Id = customer.Id,
                CreatedOn = customer.CreatedOn,
                UpdatedOn = customer.UpdatedOn,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PrimaryAdress = adress,
            };
        }

        

        /// <summary>
        /// Serializes a CustomerModel view model into a Customer data model
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public static Customer SerializeCustomer(CustomerModel customer)
        {
            var adress = new CustomerAdress
            {
                Id = customer.Id,
                CreatedOn = customer.CreatedOn,
                UpdatedOn = customer.UpdatedOn,
                AdressLine1 = customer.PrimaryAdress.AdressLine1,
                AdressLine2 = customer.PrimaryAdress.AdressLine2,
                PostalCode = customer.PrimaryAdress.PostalCode,
                State = customer.PrimaryAdress.State,
                
            };
            return new Customer
            {
                CreatedOn = customer.CreatedOn,
                UpdatedOn = customer.UpdatedOn,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PrimaryAdress = adress,
            };
        }


       
       
        

        /// <summary>
        /// Maps a CustomerAddress data model to a CustomerAddressModel view model
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>


      
    }
}
