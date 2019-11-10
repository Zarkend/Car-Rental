using System;
using System.Collections.Generic;
using System.Text;
using TestCompany.CarRental.Domain.Entities;
using TestCompany.CarRental.Domain.Requests;

namespace TestCompany.CarRental.Domain.ServiceContracts
{
    public interface IRentalService
    {
        IEnumerable<Car> GetCars();
        void RentCar(Car car);
        void ReturnCar(Car car);
        int CalculatePrice(Car car, int days);
        string ProcessRentalRequest(RentalRequest request);
    }
}
