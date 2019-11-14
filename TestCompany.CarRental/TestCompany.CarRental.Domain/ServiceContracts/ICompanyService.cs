using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TestCompany.CarRental.Domain.Entities;

namespace TestCompany.CarRental.Domain.ServiceContracts
{
    public interface ICompanyService
    {
        Task<IEnumerable<Company>> GetAsync(Expression<Func<Company, bool>> filter = null);
        Task<Company> CreateAsync(Company companyToCreate);
        Task<bool> UpdateAsync(Company companyToUpdate);
        Task<bool> DeleteAsync(Company companyToDelete);
    }
}
