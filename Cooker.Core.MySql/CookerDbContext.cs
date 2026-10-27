using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cooker.Domain;
using Microsoft.EntityFrameworkCore;

namespace Cooker.Core.MySql
{
    public class CookerDbContext : DbContext
    {

        public CookerDbContext()
        {

        }
        public CookerDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}