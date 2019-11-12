using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using TestCompany.CarRental.Domain.Entities;
using TestCompany.CarRental.Domain.Requests;

namespace TestCompany.CarRental.Infrastructure.DbContexts
{
    public class CarRentalContext : DbContext
    {
        public IConfiguration Configuration { get; }
        public DbSet<Car> Car { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<RentRequest> RentRequest { get; set; }

        public CarRentalContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
