using CodeInBlue.Entities;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        GenericRepository<Category> CategoryRepository { get; }
        GenericRepository<Customer> CustomerRepository { get; }
        GenericRepository<OrderDetail> OrderDetailRepository { get; }
        GenericRepository<Order> OrderRepository { get; }
        GenericRepository<Product> ProductRepository { get; }
        GenericRepository<Supplier> SupplierRepository { get; }
        int Completed();
    }
}
