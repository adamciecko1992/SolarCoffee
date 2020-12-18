using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using solarcoffee.services.Product;
using solarcoffee.web.ViewModels;
using AutoMapper;
using solarcoffee.data.models;

namespace solarcoffee.web.Controllers
{
    [ApiController] 
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public ProductController(ILogger<ProductController> logger, IProductService productService, IMapper mapper)
       
        {
            _logger = logger;
            _productService = productService;
            _mapper = mapper;


        }
     
        [HttpGet("/api/product")] 
        public ActionResult GetProduct()
        {

            _logger.LogInformation("gettin all products");
            var products = _productService.GetAllProducts();
            var productViewModels = products.Select((product) => _mapper.Map<ProductModel>(product));
            return Ok(productViewModels); 
        }

        [HttpPatch("/api/product/{id}")]
        public ActionResult ArchiveProduct(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _logger.LogInformation("Archiving a product");
            var archiveResult = _productService.ArchiveProduct(id);
            return Ok(archiveResult);
        }

        [HttpPost("/api/product")]
        public ActionResult SaveProduct([FromBody] ProductModel newProduct)
        {
            _logger.LogInformation("Saving new product");
            var serializedProduct = _mapper.Map<Product>(newProduct);
            _productService.CreateProduct(serializedProduct);
            return Ok(newProduct);
        }



    }
}


