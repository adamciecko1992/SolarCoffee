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
                  return new CustomerModel
            {
                Id = customer.Id,
                CreatedOn = customer.CreatedOn,
                UpdatedOn = customer.UpdatedOn,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PrimaryAdress = MapCustomerAdress(customer.PrimaryAdress),
            };
        }

        

        /// <summary>
        /// Serializes a CustomerModel view model into a Customer data model
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public static Customer SerializeCustomer(CustomerModel customer)
        {
       
            return new Customer
            {
                CreatedOn = customer.CreatedOn,
                UpdatedOn = customer.UpdatedOn,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PrimaryAdress = MapCustomerAdress(customer.PrimaryAdress),
            };
        }





        /// <summary>
        /// Maps a CustomerAddress data model to a CustomerAddressModel view model
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static CustomerAdressModel MapCustomerAdress(CustomerAdress adress)
        {
            return new CustomerAdressModel
            {
                Id = adress.Id,
                AdressLine1 = adress.AdressLine1,
                AdressLine2 = adress.AdressLine2,
                State = adress.State,
                PostalCode = adress.PostalCode,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
            };
        }

        /// <summary>
        /// Maps a CustomerAddressModel view model to a CustomerAddress data model
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static CustomerAdress MapCustomerAdress(CustomerAdressModel adress)
        {
            return new CustomerAdress
            {
                Id = adress.Id,
                CreatedOn = adress.CreatedOn,
                UpdatedOn = adress.UpdatedOn,
                AdressLine1 = adress.AdressLine1,
                AdressLine2 = adress.AdressLine2,
                PostalCode = adress.PostalCode,
                State = adress.State,
            };
        }


    }
}
