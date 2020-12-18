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
 

        public ServiceResponse<data.models.Product> ArchiveProduct(int id)
        {
            var identifiedProduct = _db.Products.Find(id);
           
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
 
        public ServiceResponse<data.models.Product> CreateProduct(data.models.Product ProvidedProduct )
        {
        
           
                _db.Products.Add(ProvidedProduct);
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

        public List<data.models.Product> GetAllProducts()
        {
        
            return _db.Products.ToList(); 
      
        }

        public data.models.Product GetProductById(int id)
        {
            return _db.Products.Find(id);
    
        }
    }
}
