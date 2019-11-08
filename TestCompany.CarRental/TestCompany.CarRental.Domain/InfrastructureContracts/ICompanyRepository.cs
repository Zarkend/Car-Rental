using System;
using System.Collections.Generic;
using System.Text;
using TestCompany.CarRental.Domain.Entities;

namespace TestCompany.CarRental.Domain.InfrastructureContracts
{
    public interface ICompanyRepository
    {
        IEnumerable<Company> GetCompanies();
        Company GetCompany(int Id);
        void UpdateCompany(Company company);
    }
}
