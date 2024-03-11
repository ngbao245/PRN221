using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise4.Data
{
    public class DBContextFactory : IDesignTimeDbContextFactory<DBContext>
    {
        public DBContext CreateDbContext(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnectString");

            var optionBuilder = new DbContextOptionsBuilder<DBContext>();
            optionBuilder.UseSqlServer(connectionString);

            return new DBContext(optionBuilder.Options);
        }
    }
}
