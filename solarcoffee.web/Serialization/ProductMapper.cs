using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using solarcoffee.web.ViewModels;

namespace solarcoffee.web.Serialization
{ /// <summary>
/// z perspektywy webApi nie chemy miec do czynienia z modelami z daty, nimi przejmuje sie entity framework,
/// dlatego kazdy model ktory dostajemy z daty musimy przemielic na taki model ktory bedzie nam potrzebny dla 
/// naszego API
/// </summary>
    public class ProductMapper { 
    
        public static ProductModel SerializeProductModel(data.models.Product productModel)
        {
            return new ProductModel
            {
                Id = productModel.Id,
                CreatedOn = productModel.CreatedOn,
                UpdatedOn = productModel.UpdatedOn,
                Name = productModel.Name,
                Description = productModel.Description,
                Price = productModel.Price,
                IsArchived = productModel.IsArchived,
                IsTaxable = productModel.IsTaxable
            };
        }
        //odwrocone
        public static data.models.Product SerializeProductModel(ProductModel product)
        {
            return new data.models.Product
            {
                Id = product.Id,
                CreatedOn = product.CreatedOn,
                UpdatedOn = product.UpdatedOn,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                IsArchived = product.IsArchived,
                IsTaxable = product.IsTaxable
            };
        }


    }
}
