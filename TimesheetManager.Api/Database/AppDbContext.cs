using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


using System.IO;


using TimesheetManager.Api.Models;




namespace TimesheetManager.Api.Database
{
    public class AppDbContext : DbContext
    {
    //     private readonly string _connectionString;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
        public DbSet<User> Users {get;set;}
        public DbSet<Customer> Customers {get;set;}
        public DbSet<Project> Projects {get;set;}
        
    }
}