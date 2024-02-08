using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Category { get; }
        ICustomerRepository Customer { get; }
        IOrderDetailRepository OrderDetail { get; }
        IOrderRepository Order { get; }
        IProductRepository Product { get; }
        ISupplierRepository Supplier { get; }
        int Completed();
    }
}
