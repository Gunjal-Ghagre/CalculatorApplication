using CalculatorApplication.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace CalculatorApplication.DatabaseContext
{
    public class CalculatorDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=CalculatorHistory;Trusted_Connection=True;");
        }

        public DbSet<Calculator> History { get; set; }
    }

}
