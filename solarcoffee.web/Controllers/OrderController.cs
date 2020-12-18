using Microsoft.AspNetCore.Mvc;
using solarcoffee.services.Order;
using solarcoffee.services.Customer;
using Microsoft.Extensions.Logging;
using solarcoffee.web.ViewModels;
using AutoMapper;
using solarcoffee.data.models;
using System.Linq;


namespace solarcoffee.web.Controllers
{
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public OrderController(ILogger<OrderController> logger, IOrderService orderService, ICustomerService customerService, IMapper mapper)
        {
            _logger = logger;
            _orderService = orderService;
            _customerService = customerService;
            _mapper = mapper;
        }
        [HttpPost("/api/invoice")]
            public ActionResult GenerateNewOrder([FromBody] InvoiceModel invoice) {
            _logger.LogInformation("Generating Invoice");
            var order = _mapper.Map<SalesOrder>(invoice);
            order.Customer = _customerService.GetById(invoice.CustomerId);
            _orderService.GenerateOpenOrder(order);
            return Ok();
        }
        [HttpGet("/api/order")]
        public ActionResult GetOrder()
        {
            var orders = _orderService.GetOrders();
            var orderModels = orders.Select((order) => _mapper.Map<OrderModel>(order));
                   
            return Ok(orderModels);
        }
        [HttpPatch("/api/order/complete/{id}")]
        public ActionResult MarkOrderComplete(int id)
        {
            _logger.LogInformation($"Marking order complete {id}");
           var fullfilled = _orderService.MarkFullfilled(id);
            return Ok(fullfilled);
        }
    }
}
