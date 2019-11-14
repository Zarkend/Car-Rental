using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TestCompany.CarRental.Domain.Entities;

namespace TestCompany.CarRental.Domain.ServiceContracts
{
    public interface ICarService
    {
        Task<IEnumerable<Car>> GetAsync(Expression<Func<Car, bool>> filter = null);
        Task<Car> CreateAsync(Car carToCreate);
        Task<bool> UpdateAsync(Car carToUpdate);
        Task<bool> DeleteAsync(Car carToDelete);
    }
}
