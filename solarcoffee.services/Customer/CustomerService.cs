using System;
using System.Collections.Generic;
using System.Linq;
using solarcoffee.data;
using Microsoft.EntityFrameworkCore;

namespace solarcoffee.services.Customer
{
    public class CustomerService : ICustomerService
    {
        private readonly solarDbContext _db;

        public CustomerService(solarDbContext dbContext)
        {
            _db = dbContext;
        }


        public ServiceResponse<data.models.Customer> CreateCustomer(data.models.Customer customer)
        {
            try
            {
                _db.Customers.Add(customer);
                _db.SaveChanges();
                return new ServiceResponse<data.models.Customer>
                {
                    IsSuccess = true,
                    Message = "New customer added",
                    Time = DateTime.UtcNow,
                    Data = customer
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<data.models.Customer>
                {
                    IsSuccess = false,
                    Message = e.StackTrace,
                    Time = DateTime.UtcNow,
                    Data = customer


                };
            }
        }

        public ServiceResponse<bool> DeleteCustomer(int id)
        {
            var customer = _db.Customers.Find(id);
            var now = DateTime.UtcNow;
           
            if (customer == null)
            {
                return new ServiceResponse<bool>
                {
                    Time = now,
                    IsSuccess = false,
                    Message = "Customer not found",
                    Data = false

                };
            }
            try
            {
               
                _db.Customers.Remove(customer);
                _db.SaveChanges();
                return new ServiceResponse<bool>
                {
                    IsSuccess = true,
                    Time = now,
                    Message = "Customer deleted",
                    Data = true,
                };
            }
            catch
            {
                return new ServiceResponse<bool>
                {
                    IsSuccess = false,
                    Time = now,
                    Message = "Customer could not be deleted",
                    Data = false,
                };
            }
        }

        public List<data.models.Customer> GetAllCustomers()
        {
            return _db.Customers
                
                 .Include((cutomer) => cutomer.PrimaryAdress)
                
                 .OrderBy((customer) => customer.LastName)
                 .ToList();
        }

        public data.models.Customer GetById(int id)
        {
           
            return _db.Customers
            .Include((c) => c.PrimaryAdress)
            .ToList()
            .Find(c => c.Id == id);
        }
    }
}
