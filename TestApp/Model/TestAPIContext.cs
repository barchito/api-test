using Microsoft.EntityFrameworkCore;
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
        public DbSet<Person> Persons { get; set; }
        public DbSet<Identifier> Identifier { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
        
    public class Response
    {
        public bool isSuccess { get; set; }
        public Object data { get; set; }
        public string message { get; set; }
    }
}
