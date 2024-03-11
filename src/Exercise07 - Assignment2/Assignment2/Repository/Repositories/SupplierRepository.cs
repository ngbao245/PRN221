using CodeInBlue.Entities;
using Repository.Data;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class SupplierRepository : GenericRepository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(Assignment2Context context) : base(context)
        {
        }
    }
}
