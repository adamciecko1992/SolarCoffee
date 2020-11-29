using System;
using System.Collections.Generic;
using System.Linq;
using solarcoffee.data;
using solarcoffee.data.models;
using solarcoffee.services.Product;

namespace solarcoffee.services
{
    public class ProductService : IProductService
    {
        private readonly solarDbContext _db;
         public ProductService(solarDbContext dbContext)
        {
            _db = dbContext;
        }
        /// <summary>
        /// Archives a product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public ServiceResponse<data.models.Product> ArchiveProduct(int id)
        {
            var identifiedProduct = _db.Products.Find(id);
           
           
                //Product jest tu przekazywany jako referencja wiec mozna go zmutowac w miejscu !!
                identifiedProduct.IsArchived = true;
                _db.SaveChanges();
                    return new ServiceResponse<data.models.Product>
                    {
                        Data = identifiedProduct,
                        Message = "Product Archived",
                        IsSuccess = true,
                        Time = DateTime.UtcNow
                    };
            
     
       

        }
        /// <summary>
        /// Adds a new product to inventory
        /// </summary>
        /// <param name="ProvidedProduct"></param>
        /// <returns></returns>
        public ServiceResponse<data.models.Product> CreateProduct(data.models.Product ProvidedProduct )
        {
        
           
                _db.Products.Add(ProvidedProduct);//add
                var newInventory = new ProductInventory
                {
                    Product = ProvidedProduct,
                    QuantityOnHand = 0,
                    IdealQuantity = 10
                };
                _db.ProductInventories.Add(newInventory);
                _db.SaveChanges();

                return new ServiceResponse<data.models.Product> { 
                Message="Solved new product",
                Data=ProvidedProduct,
                Time=DateTime.UtcNow,
                IsSuccess=true
                };
            
          
            
         
        }
        /// <summary>
        /// gets all products from db
        /// </summary>
        /// <returns></returns>
        public List<data.models.Product> GetAllProducts()
        {
        
            return _db.Products.ToList(); 
            /*
             Nasz stworzony dbContext daje nam dostep do lintowania tabel ktore stworzylismy w postgresql !
             ToList() to metoda z LINQ
             */
        }

        public data.models.Product GetProductById(int id)
        {
            return _db.Products.Find(id);
            //zwraca pojedyncza instancje produktu po id;
        }
    }
}
