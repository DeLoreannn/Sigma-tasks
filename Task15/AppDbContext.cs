using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Task15.Models;

namespace Task15
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlite(@"Data Source = Cities.db;");
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>().Navigation(x => x.Cities).AutoInclude();
            modelBuilder.Entity<City>().Navigation(x => x.Country).AutoInclude();
        }
    }
}
