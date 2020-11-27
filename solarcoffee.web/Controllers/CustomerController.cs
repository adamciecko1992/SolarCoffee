using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using solarcoffee.services.Customer;
using solarcoffee.web.ViewModels;
using solarcoffee.web.Serialization;

namespace solarcoffee.web.Controllers
{
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ILogger<CustomerController> _logger;
        private readonly ICustomerService _customerService;

        public CustomerController( ILogger<CustomerController> logger, ICustomerService customerService) 
        {
            _logger = logger;
            _customerService = customerService;

        }
        [HttpPost("api/customer")]
        public ActionResult CreateCustomer([FromBody] CustomerModel customer)
        {
            if (customer== null)
            {
                _logger.LogInformation("No customer provided", customer);

                return Problem();
            }
            else
            {
                _logger.LogInformation("Creating Customer", customer.ToString());
                //dopisz do uzyskanego obiektu z body z requesta dateTimy
                customer.CreatedOn = DateTime.UtcNow;
                customer.UpdatedOn = DateTime.UtcNow;
                //przerob otrzymanego cutomera z viewModelCustomera na dataModelCustomera
                var customerData = CustomerMapper.SerializeCustomer(customer);
                //stworz nowego customera za pomoca metody w servisie 
                var newCustomer = _customerService.CreateCustomer(customerData);
                //zwroc odpowiedz z servera 
                return Ok(newCustomer);
            }
        }
        [HttpGet("api/customer")]
        public ActionResult GetAllCustomers()
        {
            _logger.LogInformation("Getting all customers");
            //pobierz dane wszystkich uzytkownikow jako modele data
            var customers = _customerService.GetAllCustomers();
            var SerializedCustomers = customers.Select((customer) => new CustomerModel
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Id = customer.Id,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
                PrimaryAdress = CustomerMapper.MapCustomerAdress(customer.PrimaryAdress)
            }).OrderByDescending((customer) => customer.CreatedOn)
           .ToList();
            return Ok(SerializedCustomers);
        }

        [HttpDelete("api/customer/{id}")]
        public ActionResult DeleteCustomer(int id)
        {
            _logger.LogInformation("Deleting customer");
            var response = _customerService.DeleteCustomer(id);
            return Ok(response);
        }
        [HttpGet("api/customer/{id}")]
        public ActionResult GetById(int id)
        {
            _logger.LogInformation("RecivingCustomer");
            var response = _customerService.GetById(id);
            return Ok(response);
        }
    }
}
