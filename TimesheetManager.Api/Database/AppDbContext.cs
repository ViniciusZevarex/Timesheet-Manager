using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


using System.IO;


using TimesheetManager.Api.Models;




namespace TimesheetManager.Api.Database
{
    public class AppDbContext : DbContext
    {
        private readonly string _connectionString;

        public AppDbContext()
        {
            _connectionString = GetConfiguration().GetSection("ConnectionString").Value;
        }



        //Tables
        public DbSet<User> Users {get;set;}





        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }


        public IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional:true,reloadOnChange:true);

            return builder.Build();
        }

    }
}