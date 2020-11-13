using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace solarcoffee.services.Product
{
    //definiuje zachowanie classy ktora implementuje interface
    // obsluga zadania dla endpointu z produktami
   public interface IProductService
    {
        List<data.models.Product> GetAllProducts();
        data.models.Product GetProductById(int id);
        ServiceResponse<data.models.Product> CreateProduct(data.models.Product product);
        ServiceResponse<data.models.Product> ArchiveProduct(int id);
    }
}
