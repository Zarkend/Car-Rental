using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TestCompany.CarRental.Domain.Entities;
using TestCompany.CarRental.Domain.Enums;
using TestCompany.CarRental.Domain.InfrastructureContracts;
using TestCompany.CarRental.Infrastructure.DbContexts;

namespace TestCompany.CarRental.Infrastructure.Repositories
{
    public class CompanyRepository : BaseRepository<Company>
    {
        private CarRentalContext _context;

        public CompanyRepository(CarRentalContext context) : base(context)
        {
            _context = context;
        }

        public override void Insert(Company company)
        {
            company.CreatedDate = DateTime.Now;
            company.UpdatedDate = DateTime.Now;
            base.Insert(company);
        }

        public override void Update(Company company)
        {
            company.UpdatedDate = DateTime.Now;
            base.Update(company);
        }

        public void DeleteAll()
        {
            _context.Database.ExecuteSqlRaw("DELETE FROM Company");
        }

        public override void FillTestData()
        {
            List<Company> Companies = new List<Company>();

            Companies.Add(new Company() { Name = "Vueling Airlines" });
            Companies.Add(new Company() { Name = "Blizzard" });
            Companies.Add(new Company() { Name = "Riot Games" });
            Companies.Add(new Company() { Name = "King" });
            Companies.Add(new Company() { Name = "ToySRUs" });
            Companies.Add(new Company() { Name = "Decathlon" });

            DeleteAll();

            Companies.ForEach(Insert);

            _context.SaveChanges();
        }
    }
}
