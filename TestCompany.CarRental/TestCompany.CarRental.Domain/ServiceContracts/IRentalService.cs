using System;
using System.Collections.Generic;
using System.Text;
using TestCompany.CarRental.Domain.Entities;

namespace TestCompany.CarRental.Domain.ServiceContracts
{
    public interface IRentalService
    {
        void RentCar(Car car, Company company);
        void ReturnCar(Car car);
    }
}
