using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp.Model
{
    public class TestAPIContext : DbContext
    {
        public TestAPIContext(DbContextOptions options)
            : base(options)
        { }
        public DbSet<Person> People { get; set; }
        public DbSet<Identifier> Identifiers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
         
        }


    }


}
