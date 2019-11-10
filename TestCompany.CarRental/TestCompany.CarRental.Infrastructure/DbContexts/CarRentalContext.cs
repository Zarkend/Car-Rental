using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TestCompany.CarRental.Domain.Entities;
using TestCompany.CarRental.Domain.Requests;

namespace TestCompany.CarRental.Infrastructure.DbContexts
{
    public class CarRentalContext : DbContext
    {
        public DbSet<Car> Car { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<CarConfig> CarConfig { get; set; }
        public DbSet<RentalRequest> RentalRequest { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlServer(@"Server=ANTONIO-PC\SQLEXPRESS;Database=CarRental;User Id=CarRental;Password=1234;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarConfig>().HasNoKey();
        }
    }
}
