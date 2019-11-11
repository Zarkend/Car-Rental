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
    public class CompanyRepository : IRepository<Company>
    {
        private CarRentalContext _context;

        public CompanyRepository(CarRentalContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
            GC.SuppressFinalize(this);
        }

        public IQueryable<Company> Query(Expression<Func<Company, bool>> filter)
        {
            return _context.Company.Where(filter);
        }

        public Company GetById(int id)
        {
            return _context.Company.Find(id);
        }

        public IEnumerable<Company> GetAll()
        {
            return _context.Company.ToList();
        }

        public void Insert(Company company)
        {
            company.CreatedDate = DateTime.Now;
            company.UpdatedDate = DateTime.Now;
            _context.Company.Add(company);
        }

        public void Update(Company company)
        {
            company.UpdatedDate = DateTime.Now;
            _context.Entry(company).State = EntityState.Modified;
        }

        public void Delete(int Id)
        {
            Company company = _context.Company.Find(Id);
            _context.Company.Remove(company);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void DeleteAll()
        {
            _context.Database.ExecuteSqlRaw("DELETE FROM Company");
        }

        public void FillTestData()
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
