using CodeInBlue.Entities;
using Repository.Data;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Assignment2Context _context;
        private GenericRepository<Category> _categoryRepository;
        private GenericRepository<Customer> _customerRepository;
        private GenericRepository<OrderDetail> _orderDetailRepository;
        private GenericRepository<Order> _orderRepository;
        private GenericRepository<Product> _productRepository;
        private GenericRepository<Supplier> _supplierRepository;

        public UnitOfWork(Assignment2Context context)
        {
            _context = context;
        }

        public GenericRepository<Category> CategoryRepository
        {
            get
            {
                return _categoryRepository ??= new GenericRepository<Category>(_context);
            }
        }

        public GenericRepository<Customer> CustomerRepository
        {
            get
            {
                return _customerRepository ??= new GenericRepository<Customer>(_context);
            }
        }

        public GenericRepository<OrderDetail> OrderDetailRepository
        {
            get
            {
                return _orderDetailRepository ??= new GenericRepository<OrderDetail>(_context);
            }
        }

        public GenericRepository<Order> OrderRepository
        {
            get
            {
                return _orderRepository ??= new GenericRepository<Order>(_context);
            }
        }

        public GenericRepository<Product> ProductRepository
        {
            get
            {
                return _productRepository ??= new GenericRepository<Product>(_context);
            }
        }

        public GenericRepository<Supplier> SupplierRepository
        {
            get
            {
                return _supplierRepository ??= new GenericRepository<Supplier>(_context);
            }
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
