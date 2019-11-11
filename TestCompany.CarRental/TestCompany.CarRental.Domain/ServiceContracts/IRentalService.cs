using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TestCompany.CarRental.Domain.Entities;
using TestCompany.CarRental.Domain.Entities.Responses;
using TestCompany.CarRental.Domain.Requests;

namespace TestCompany.CarRental.Domain.ServiceContracts
{
    public interface IRentalService
    {
        void RentCar(Car car, int companyId);
        ReturnCarResponse ReturnCars(IEnumerable<int> carIds);
        string ProcessRentalRequest(RentalRequest request);
    }
}
