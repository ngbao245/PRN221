using BaoNH.ASM2.Repo.Entities;

namespace BaoNH.ASM2.Repo.Repositories
{
    public class UnitOfWork
    {
        private readonly LoginSessionContext _context;
        private GenericRepository<User> _userRepository;

        public UnitOfWork(LoginSessionContext context)
        {
            _context = context;
        }

        public GenericRepository<User> UserRepository
        {
            get
            {
                return _userRepository ??= new GenericRepository<User>(_context);
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
