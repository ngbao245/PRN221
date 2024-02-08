using Repository.Data;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Assignment2Context _context;
        public ICategoryRepository Category { get; }
        public ICustomerRepository Customer { get; }
        public IOrderDetailRepository OrderDetail { get; }
        public IOrderRepository Order { get; }
        public IProductRepository Product { get; }
        public ISupplierRepository Supplier { get; }

        public UnitOfWork(Assignment2Context context)
        {
            _context = context;
            Category = new CategoryRepository(_context);
            Customer = new CustomerRepository(_context);
            OrderDetail = new OrderDetailRepository(_context);
            Order = new OrderRepository(_context);
            Product = new ProductRepository(_context);
            Supplier = new SupplierRepository(_context);
        }

        public int Completed()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
