using Microsoft.AspNetCore.Mvc;
using solarcoffee.services.Order;
using solarcoffee.services.Customer;
using Microsoft.Extensions.Logging;
using solarcoffee.web.ViewModels;
using solarcoffee.web.Serialization;

namespace solarcoffee.web.Controllers
{
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;

        public OrderController(ILogger<OrderController> logger, IOrderService orderService, ICustomerService customerService)
        {
            _logger = logger;
            _orderService = orderService;
            _customerService = customerService;
        }
        [HttpPost("/api/invoice")]
        //[FromBody] to decorator który zaznacza ze tresc ma byc wyciagnieta z body requesta, poki ma odpowiednia strukture bedzie smigac
        public ActionResult GenerateNewOrder([FromBody] InvoiceModel invoice) {
            _logger.LogInformation("Generating Invoice");
            var order = OrderMapper.SerializeInvoiceToOrder(invoice); //sales order data model
            order.Customer = _customerService.GetById(invoice.CustomerId);
            _orderService.GenerateOpenOrder(order);
            return Ok();
        }
        [HttpGet("/api/order")]
        public ActionResult GetOrder()
        {
            var orders = _orderService.GetOrders();
            var orderModels = OrderMapper.SerializeOrderToViewModels(orders);
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
