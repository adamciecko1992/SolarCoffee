using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarcoffee.services.Customer
{
    interface ICustomerService
    {
        List<data.models.Customer> GetAllCustomers();
        ServiceResponse<data.models.Customer> CreateCustomer(data.models.Customer customer);
        ServiceResponse<bool> DeleteCustomer(int id);
        data.models.Customer GetById(int id);
    }
}
