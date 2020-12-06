using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using solarcoffee.services.Customer;
using solarcoffee.web.ViewModels;
using AutoMapper;
using solarcoffee.data.models;

namespace solarcoffee.web.Controllers
{
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public CustomerController(ILogger<CustomerController> logger, ICustomerService customerService, IMapper mapper)
        {
            _logger = logger;
            _customerService = customerService;
            _mapper = mapper;
        }
        [HttpPost("api/customer")]
        public ActionResult CreateCustomer([FromBody] CustomerModel customer)
        {
            if (customer == null)
            {
                _logger.LogInformation("No customer provided", customer);

                return Problem();
            }
            else
            {
                //dopisz do uzyskanego obiektu z body z requesta dateTimy
                customer.CreatedOn = DateTime.UtcNow;
                customer.UpdatedOn = DateTime.UtcNow;


                var customerData = _mapper.Map<Customer>(customer);
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

            var SerializedCustomers = customers.Select((customer) => _mapper.Map<CustomerModel>(customer))
                .OrderByDescending((customer) => customer.CreatedOn)
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
