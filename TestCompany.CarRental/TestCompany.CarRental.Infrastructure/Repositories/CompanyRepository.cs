using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
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

        public async override Task InsertAsync(Company company)
        {
            company.CreatedDate = DateTime.Now;
            company.UpdatedDate = DateTime.Now;
            await base.InsertAsync(company);
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

        public async override Task FillTestDataAsync()
        {
            List<Company> Companies = new List<Company>();

            Companies.Add(new Company() { Name = "Vueling Airlines", CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now });
            Companies.Add(new Company() { Name = "Blizzard", CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now });
            Companies.Add(new Company() { Name = "Riot Games", CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now });
            Companies.Add(new Company() { Name = "King", CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now });
            Companies.Add(new Company() { Name = "ToySRUs", CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now });
            Companies.Add(new Company() { Name = "Decathlon", CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now });

            DeleteAll();

            Companies.ForEach(async x=> await InsertAsync(x));

            _context.SaveChanges();
        }
    }
}
