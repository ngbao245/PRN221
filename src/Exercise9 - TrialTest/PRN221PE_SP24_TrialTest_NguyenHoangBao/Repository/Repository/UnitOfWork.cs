using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class UnitOfWork
    {
        private readonly Eyeglasses2024DBContext _context;
        private GenericRepository<Eyeglass> _eyeGlassRepository;
        private GenericRepository<LensType> _lensTypeRepository;
        private GenericRepository<StoreAccount> _storeAccountRepository;

        public UnitOfWork(Eyeglasses2024DBContext context)
        {
            _context = context;
        }

        public GenericRepository<Eyeglass> EyeGlassRepository
        {
            get
            {
                return _eyeGlassRepository ??= new GenericRepository<Eyeglass>(_context);
            }
        }

        public GenericRepository<LensType> LensTypeRepository
        {
            get
            {
                return _lensTypeRepository ??= new GenericRepository<LensType>(_context);
            }
        }

        public GenericRepository<StoreAccount> StoreAccountRepository
        {
            get
            {
                return _storeAccountRepository ??= new GenericRepository<StoreAccount>(_context);
            }
        }
        public int Completed()
        {
            return _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
