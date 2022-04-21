using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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

        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("WebApiDatabase"));
            //#region ConnectionStrings
            //// Laptop
            //string connectionString =
            //    @"Data Source=DESKTOP-EJ7V12L\SQLEXPRESS01;" +
            //    @"Initial Catalog = MegaGameEventManager;" +
            //    @"Integrated Security=true";

            ////Stationär
            ////string connectionString =
            ////    @"Data Source=DESKTOP-JC3MCVE;" +
            ////    @"Initial Catalog = MegaGameEventManager;" +
            ////    @"Integrated Security=true";
            //#endregion

            //base.OnConfiguring(optionsBuilder);
        }
    }
}
