using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IProductService
    {
        IEnumerable<ProductModel> GetAll();
        bool AddProduct(ProductModel model);
        bool DeleteProduct(int id);
    }
}
