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
    public static class ProductMapper {

        public static ProductModel SerializeProductModel(data.models.Product product)
        {
            return new ProductModel
            {
                Id = product.Id,
                CreatedOn = product.CreatedOn,
                UpdatedOn = product.CreatedOn,
                Price = product.Price,
                Name = product.Name,
                Description = product.Description,
                IsTaxable = product.IsTaxable,
                IsArchived = product.IsArchived
            };
        }
        //odwrocone
        public static data.models.Product SerializeProductModel(ProductModel product)
        {
            return new data.models.Product
            {
                Id = product.Id,
                CreatedOn = product.CreatedOn,
                UpdatedOn = product.CreatedOn,
                Price = product.Price,
                Name = product.Name,
                Description = product.Description,
                IsTaxable = product.IsTaxable,
                IsArchived = product.IsArchived
            };
        }

    }
}
