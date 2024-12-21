using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafm.Infrastructure.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CafmContext>
    {
        public CafmContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CafmContext>();

            // Use the connection string configured for your Azure SQL Database
            optionsBuilder.UseSqlServer("your connection string");

            return new CafmContext(optionsBuilder.Options);
        }
    }
}
