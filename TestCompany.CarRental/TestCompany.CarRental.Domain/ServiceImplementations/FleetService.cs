using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TestCompany.CarRental.Domain.Entities;
using TestCompany.CarRental.Domain.InfrastructureContracts;
using TestCompany.CarRental.Domain.ServiceContracts;

namespace TestCompany.CarRental.Domain.ServiceImplementations
{
    public class FleetService : IFleetService
    {
        private readonly IRepository<Car> _carRepository;
        public FleetService(IRepository<Car> carRepository)
        {
            _carRepository = carRepository;
        }
        public IEnumerable<Car> GetCars()
        {
            return _carRepository.GetAll();
        }

        public IQueryable<Car> Query(Expression<Func<Car, bool>> filter)
        {
            return _carRepository.Query(filter);
        }
    }
}
