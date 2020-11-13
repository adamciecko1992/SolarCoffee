using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using solarcoffee.services.Product;
using solarcoffee.web.Serialization;


namespace solarcoffee.web.Controllers
{
    [ApiController] //oznaczenie ae to kontroller
    public class ProductController :ControllerBase //wbija podstawowe funkcjonalności, jak requesty responsy,httpContext
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;
        public ProductController(ILogger<ProductController> logger, IProductService productService)
            //Injecting ProductController as ILogger
            //Injecting productService  - more details in startup and service.AddTransient()
            //nie trzeba tworzyc instancji kontrollerow framework robi to za nas 
        {
            _logger = logger;
            _productService = productService;


        }
        //Action result to jest to co zwraca nasz controller, moze byc json, albo view albo inne
        [HttpGet("/api/product")] //po requescie do tej sciezki wydary sie metoda
        public ActionResult GetProduct() {
            _logger.LogInformation("gettin all products");
            var products =  _productService.GetAllProducts();
            //jak map w JS
            var productViewModels = products.Select((product) => ProductMapper.SerializeProductModel(product));
            return Ok(productViewModels); //Ok to ActionResult, a w argumencie przyjmuje tresc ktora ma zwrocic
        }
    }
}

//do mapowania modeli na viewmodele sluzy automapperframework
