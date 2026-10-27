using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cooker.Core.MySql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Cooker.Core
{
    public class CookerDbContextFactory : IDesignTimeDbContextFactory<CookerDbContext>
    {
        public CookerDbContext CreateDbContext(string[] args)
        {
            var currentDir = Directory.GetCurrentDirectory();
            var configuration = new ConfigurationBuilder()
        .SetBasePath(currentDir)
        .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
        .Build();

            var connectionString = configuration.GetConnectionString("Default");

            var optionsBuilder = new DbContextOptionsBuilder<CookerDbContext>();
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            return new CookerDbContext(optionsBuilder.Options);
        }
    }
}