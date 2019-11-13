using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TestCompany.CarRental.Domain.Entities;

namespace TestCompany.CarRental.Domain.ServiceContracts
{
    public interface IFleetService
    {
        Task<IEnumerable<Car>> GetAsync(Expression<Func<Car, bool>> filter = null);
        Task<bool> UpdateCarAsync(Car carToUpdate);
        Task<bool> DeleteCarAsync(Car carToUpdate);
    }
}
