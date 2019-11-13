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
    public class FleetService : IFleetService
    {
        private readonly IUnitOfWork _unitOfWork;
        public FleetService(IUnitOfWork unitOfWork)
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

        public async Task<bool> UpdateCarAsync(Car carToUpdate)
        {
            _unitOfWork.Cars.Update(carToUpdate);
            var updated = await _unitOfWork.CommitAsync();
            return updated > 0;
        }
        public async Task<bool> DeleteCarAsync(Car carToDelete)
        {
            _unitOfWork.Cars.Delete(carToDelete);
            var deleted = await _unitOfWork.CommitAsync();
            return deleted > 0;
        }
    }
}
