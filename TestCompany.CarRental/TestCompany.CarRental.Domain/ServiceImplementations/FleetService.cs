using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
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
        public IEnumerable<Car> GetCars()
        {
            return _unitOfWork.Cars.Get();
        }

        public IEnumerable<Car> Get(Expression<Func<Car, bool>> filter = null)
        {
            return _unitOfWork.Cars.Get(filter);
        }
    }
}
