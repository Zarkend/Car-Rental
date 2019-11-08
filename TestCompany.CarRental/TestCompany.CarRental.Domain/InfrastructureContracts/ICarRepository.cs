using System;
using System.Collections.Generic;
using System.Text;
using TestCompany.CarRental.Domain.Entities;

namespace TestCompany.CarRental.Domain.InfrastructureContracts
{
    public interface ICarRepository
    {
        IEnumerable<Car> GetCars();
        Car GetCar(int Id);
        void UpdateCar(Car car);
        CarConfig GetCarConfig(Car car);
        int GetCarPricePerDay(Car car);
    }
}
