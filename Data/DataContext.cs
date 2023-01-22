using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPITextRPG.Data
{
    public class DataContext : DbContext
    {   //creating a constructor
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Character> Characters { get; set; } //creating DbSet so that Entity Framework knows what tables to create
        public DbSet<User> Users { get; set; }
    }
}