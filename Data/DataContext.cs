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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Spell>().HasData(
                new Spell { Id = 1, Name = "Fireball", Damage = 30 },
                new Spell { Id = 2, Name = "Blizzard", Damage = 25 },
                new Spell { Id = 3, Name = "Thuderwave", Damage = 35 }
            );
        }

        public DbSet<Character> Characters { get; set; } //creating DbSet so that Entity Framework knows what tables to create
        public DbSet<User> Users { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<Spell> Spells { get; set; }
    }
}