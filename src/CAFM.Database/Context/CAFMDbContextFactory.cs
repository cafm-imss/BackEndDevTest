using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
namespace CAFM.Database.Context;

public class CAFMDbContextFactory : IDesignTimeDbContextFactory<CAFMDbContext>
{
    public CAFMDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CAFMDbContext>();
        optionsBuilder.UseSqlServer("Server=your_server_name;Database=your_database_name;Trusted_Connection=True;");
        return new CAFMDbContext(optionsBuilder.Options);
    }
}
