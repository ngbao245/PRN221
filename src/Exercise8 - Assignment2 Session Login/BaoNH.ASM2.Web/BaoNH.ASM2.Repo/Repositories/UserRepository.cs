using BaoNH.ASM2.Repo.Entities;

namespace BaoNH.ASM2.Repo.Repositories
{
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository(LoginSessionContext context) : base(context)
        {
        }
    }
}
