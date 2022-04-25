using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class DataContext : DbContext
    {
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Faction> Factions { get; set; }
        //public DbSet<User> Users { get; set; }

        public DataContext() { }

        public DataContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #region ConnectionStrings
            // Laptop
            string connectionString =
                @"Data Source=DESKTOP-EJ7V12L\SQLEXPRESS01;" +
                @"Initial Catalog = MegaGameEventManager;" +
                @"Integrated Security=true";

            //Stationär
            //string connectionString =
            //    @"Data Source=DESKTOP-JC3MCVE;" +
            //    @"Initial Catalog = MegaGameEventManager;" +
            //    @"Integrated Security=true";
            #endregion

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetupActivity(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        private static void SetupActivity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activity>().Property(a => a.StartDate).IsRequired();
            modelBuilder.Entity<Activity>().Property(a => a.EndDate).IsRequired();
        }
    }
}
