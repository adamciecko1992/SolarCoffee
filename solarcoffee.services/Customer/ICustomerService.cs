using System.Collections.Generic;

namespace solarcoffee.services.Customer
{
    public interface ICustomerService
    {
        List<data.models.Customer> GetAllCustomers();
        ServiceResponse<data.models.Customer> CreateCustomer(data.models.Customer customer);
        ServiceResponse<bool> DeleteCustomer(int id);
        data.models.Customer GetById(int id);
    }
}
