using System;
using System.Collections.Generic;
using System.Text;
using TestCompany.CarRental.Domain.Entities;
using TestCompany.CarRental.Domain.InfrastructureContracts;

namespace TestCompany.CarRental.Infrastructure.Repositories
{
    public class CarRepository : ICarRepository
    {
        private List<Car> _fleet = new List<Car>();

        public Car GetCar(int Id)
        {
            throw new NotImplementedException();
        }

        public CarRates GetCarRates(Car car)
        {
            throw new NotImplementedException();
        }

        public void UpdateCar(Car car)
        {
            throw new NotImplementedException();
        }
    }
}
