using System;
using System.Collections.Generic;
using System.Text;
using TestCompany.CarRental.Domain.Entities;

namespace TestCompany.CarRental.Domain.InfrastructureContracts
{
    public interface ICarRepository
    {
        Car GetCar(int Id);
        void UpdateCar(Car car);
        CarRates GetCarRates(Car car);
    }
}
