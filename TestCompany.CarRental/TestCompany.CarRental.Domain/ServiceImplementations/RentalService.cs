using System;
using System.Collections.Generic;
using System.Text;
using TestCompany.CarRental.Domain.Entities;
using TestCompany.CarRental.Domain.InfrastructureContracts;
using TestCompany.CarRental.Domain.ServiceContracts;

namespace TestCompany.CarRental.Domain.ServiceImplementations
{
    public class RentalService : IRentalService
    {
        private ICarRepository _carRepository;

        public RentalService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }
        public void RentCar(Car car, Company company)
        {
            _carRepository.UpdateCar(car);
        }
        public void ReturnCar(Car car)
        {
            _carRepository.UpdateCar(car);
        }
    }
}
