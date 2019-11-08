using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using TestCompany.CarRental.Domain.Entities;
using TestCompany.CarRental.Domain.Enums;
using TestCompany.CarRental.Domain.InfrastructureContracts;

namespace TestCompany.CarRental.Infrastructure.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
             

        public IEnumerable<Company> GetCompanies()
        {
            throw new NotImplementedException();
        }

        public Company GetCompany(int Id)
        {
            throw new NotImplementedException();
        }

        public void UpdateCompany(Company company)
        {
            throw new NotImplementedException();
        }
    }
}
