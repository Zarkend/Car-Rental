using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TestCompany.CarRental.Domain.Entities;
using TestCompany.CarRental.Domain.ServiceContracts;
using TestCompany.CarRental.Domain.UnitOfWork;

namespace TestCompany.CompanyRental.Domain.ServiceImplementations
{
    public class CompanyService : ICompanyService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompanyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Company>> GetCompanysAsync()
        {
            return await _unitOfWork.Companies.GetAsync();
        }

        public async Task<IEnumerable<Company>> GetAsync(Expression<Func<Company, bool>> filter = null)
        {
            return await _unitOfWork.Companies.GetAsync(filter);
        }
        public async Task<Company> CreateAsync(Company companyToCreate)
        {
            await _unitOfWork.Companies.InsertAsync(companyToCreate);
            var created = await _unitOfWork.CommitAsync();

            if (created > 0)
                return _unitOfWork.Companies.Entry(companyToCreate);
            else
               return null;
        }

        public async Task<bool> UpdateAsync(Company companyToUpdate)
        {
            _unitOfWork.Companies.Update(companyToUpdate);
            var updated = await _unitOfWork.CommitAsync();
            return updated > 0;
        }
        public async Task<bool> DeleteAsync(Company companyToDelete)
        {
            _unitOfWork.Companies.Delete(companyToDelete);
            var deleted = await _unitOfWork.CommitAsync();
            return deleted > 0;
        }
    }
}
