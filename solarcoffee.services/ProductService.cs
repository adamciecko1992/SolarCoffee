using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            try
            {
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
            catch(Exception e)
            {
                return new ServiceResponse<data.models.Product>
                {
                    Data = null, //nie produkt bo moze go nawet nie znalezlismy po id
                    Message = "Product not Archived",
                    IsSuccess = true,
                    Time = DateTime.UtcNow
                };
            }
       

        }
        /// <summary>
        /// Adds a new product to inventory
        /// </summary>
        /// <param name="ProvidedProduct"></param>
        /// <returns></returns>
        public ServiceResponse<data.models.Product> CreateProduct(data.models.Product ProvidedProduct )
        {
            try
            {
                _db.Products.Add(ProvidedProduct);//add
                var newInventory = new ProductInventory
                { //dzieki entity mozemy przypisac w tym miejscu properties
                    Product = ProvidedProduct,
                    QuantityOnHand = 0,
                    IdealQuantity = 10
                };
                _db.ProductInventories.Add(newInventory);
                _db.SaveChanges();//commit

                return new ServiceResponse<data.models.Product> { 
                Message="Solved new product",
                Data=ProvidedProduct,
                Time=DateTime.UtcNow,
                IsSuccess=true
                };
            }
            catch(Exception e)
            {
                return new ServiceResponse<data.models.Product>
                {
                    Message = "Error on adding a product",
                    Data = ProvidedProduct,
                    Time = DateTime.UtcNow,
                    IsSuccess = false
                };
            }
          
            
            //dodaje produkt do 'kolejki' i 'scomituje' zmiane do bazy danych po db.SaveChanges()
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
