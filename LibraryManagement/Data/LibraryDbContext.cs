using LibraryManagement.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Data
{
    public class LibraryDbContext: DbContext 
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options): base(options)
        {

        }

        public DbSet<Catalog> Catalogs { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Pipeline> Pipelines { get; set; }
        public DbSet<OrderLog> OrderLogs { get; set; }
        public DbSet<JenkinPipeline> JenkinPipelines { get; set; }
    }
}
