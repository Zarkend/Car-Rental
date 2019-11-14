using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TestCompany.CarRental.Domain.Entities;
using TestCompany.CarRental.Domain.InfrastructureContracts;
using TestCompany.CarRental.Domain.ServiceContracts;
using TestCompany.CarRental.Domain.UnitOfWork;

namespace TestCompany.CarRental.Domain.ServiceImplementations
{
    public class CarService : ICarService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CarService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Car>> GetCarsAsync()
        {
            return await _unitOfWork.Cars.GetAsync();
        }

        public async Task<IEnumerable<Car>> GetAsync(Expression<Func<Car, bool>> filter = null)
        {
            return await _unitOfWork.Cars.GetAsync(filter);
        }
        public async Task<Car> CreateAsync(Car carToCreate)
        {
            await _unitOfWork.Cars.InsertAsync(carToCreate);
            var created = await _unitOfWork.CommitAsync();

            if (created > 0)
                return _unitOfWork.Cars.Entry(carToCreate);
            else
               return null;
        }

        public async Task<bool> UpdateAsync(Car carToUpdate)
        {
            _unitOfWork.Cars.Update(carToUpdate);
            var updated = await _unitOfWork.CommitAsync();
            return updated > 0;
        }
        public async Task<bool> DeleteAsync(Car carToDelete)
        {
            _unitOfWork.Cars.Delete(carToDelete);
            var deleted = await _unitOfWork.CommitAsync();
            return deleted > 0;
        }
    }
}
