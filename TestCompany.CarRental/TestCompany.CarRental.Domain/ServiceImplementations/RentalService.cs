using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestCompany.CarRental.Domain.Entities;
using TestCompany.CarRental.Domain.Enums;
using TestCompany.CarRental.Domain.InfrastructureContracts;
using TestCompany.CarRental.Domain.Requests;
using TestCompany.CarRental.Domain.ServiceContracts;

namespace TestCompany.CarRental.Domain.ServiceImplementations
{
    public class RentalService : IRentalService
    {
        private ICarRepository _carRepository;
        private ICompanyRepository _companyRepository;

        public RentalService(ICarRepository carRepository, ICompanyRepository companyRepository)
        {
            _carRepository = carRepository;
            _companyRepository = companyRepository;
        }



        public IEnumerable<Car> GetCars()
        {
            return _carRepository.GetCars();
        }

        public void RentCar(Car car)
        {
            car.Rented = true;
            car.RentedDate = DateTime.Now;
            _carRepository.UpdateCar(car);
        }
        public void ReturnCar(Car car)
        {
            _carRepository.UpdateCar(car);
        }
        public int CalculatePrice(Car car, int days)
        {
            int price = 0;

            int carPricePerDay = _carRepository.GetCarPricePerDay(car);

            price = carPricePerDay * days;

            return price;           
        }

        public string ProcessRentalRequest(RentalRequest request)
        {
            string result = "";

            IEnumerable<Car> cars = GetCars();

            if (request.Brand != Brand.Undefined)
                cars = cars.Where(x => x.Brand == request.Brand);

            if (request.Type != CarType.Undefined)
                cars = cars.Where(x => x.Type == request.Type);

            if (cars.Count() < request.Amount)
                result = "Not Enought Cars";

            List<Car> carsToRent = cars.Take(request.Amount).ToList();

           
            carsToRent.ForEach(x => {
                x.CompanyId = request.CompanyId;
                RentCar(x);
                });

            result = "Rented successfully!";

            return result;
        }
    }
}
