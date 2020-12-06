﻿using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using solarcoffee.services.Product;
using solarcoffee.web.ViewModels;
using AutoMapper;
using solarcoffee.data.models;

namespace solarcoffee.web.Controllers
{
    [ApiController] //oznaczenie ae to kontroller
    public class ProductController : ControllerBase //wbija podstawowe funkcjonalności, jak requesty responsy,httpContext
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public ProductController(ILogger<ProductController> logger, IProductService productService, IMapper mapper)
        //Injecting ProductController as ILogger
        //Injecting productService  - more details in startup and service.AddTransient()
        //nie trzeba tworzyc instancji kontrollerow framework robi to za nas 
        {
            _logger = logger;
            _productService = productService;
            _mapper = mapper;


        }
        //Action result to jest to co zwraca nasz controller, moze byc json, albo view albo inne
        [HttpGet("/api/product")] //po requescie do tej sciezki wydary sie metoda
        public ActionResult GetProduct()
        {

            _logger.LogInformation("gettin all products");
            var products = _productService.GetAllProducts();
            var productViewModels = products.Select((product) => _mapper.Map<ProductModel>(product));
            return Ok(productViewModels); //Ok to ActionResult, a w argumencie przyjmuje tresc ktora ma zwrocic
        }

        //tutaj id jest dynamiczne i mapuje sie do id uzytego w metodzie
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

//do mapowania modeli na viewmodele sluzy automapperframework
