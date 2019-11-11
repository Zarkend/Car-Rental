using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TestCompany.CarRental.Domain.Entities;
using TestCompany.CarRental.Domain.Entities.Responses;
using TestCompany.CarRental.Domain.Enums;
using TestCompany.CarRental.Domain.InfrastructureContracts;
using TestCompany.CarRental.Domain.Requests;
using TestCompany.CarRental.Domain.ServiceContracts;

namespace TestCompany.CarRental.Domain.ServiceImplementations
{
    public class RentalService : IRentalService
    {
        private IRepository<Car> _carRepository;
        private IRepository<Company> _companyRepository;
        private IRepository<RentalRequest> _rentalRequestRepository;
        private IFleetService _fleetService;

        public RentalService(IRepository<Car> carRepository, IRepository<Company> companyRepository, IRepository<RentalRequest> rentalRequestRepository, IFleetService fleetService)
        {
            _carRepository = carRepository;
            _companyRepository = companyRepository;
            _rentalRequestRepository = rentalRequestRepository;
            _fleetService = fleetService;
        }
        
        public void RentCar(Car car, int companyId)
        {
            car.CompanyId = companyId;
            car.Rented = true;
            car.RentedDate = DateTime.Now;
            _carRepository.Update(car);
            _carRepository.Save();
        }
        public ReturnCarResponse ReturnCars(IEnumerable<int> carIds)
        {
            ReturnCarResponse response = new ReturnCarResponse();
            response.Status = ReturnCarResponseStatus.Succeded.ToString();

            foreach (var carId in carIds)
            {
                Car car = _carRepository.GetById(carId);

                if(car == null)
                {
                    ReturnCarResponseMarkAsNotFound(response,carId);
                    return response;
                }

                if (!car.Rented) {
                    ReturnCarResponseMarkAsNotRented(response, carId);
                    return response;
                }

                if (DateTime.Now.Date > car.RentedUntilDate.Value.Date)
                {
                    ReturnCarResponseAddExtraCost(response, car);
                }
                else
                {
                    response.CarResults.Add(new ReturnCarResult()
                    {
                        Message = $"Car with Id {carId} returned correctly.",
                        Status = ReturnCarStatus.Succeded.ToString()
                    });
                }

                car.Rented = false;
                car.RentedUntilDate = null;
                car.RentedDate = null;
                car.CompanyId = null;
                _carRepository.Update(car);
                _carRepository.Save();
            }

            return response;
        }

        public string ProcessRentalRequest(RentalRequest request)
        {
            string result = "";
            bool rentalSucceded = false;

            IEnumerable<Car> cars = _fleetService.GetCars().Where(x=> !x.Rented);

            if (request.Brand != Brand.Undefined)
                cars = cars.Where(x => x.Brand == request.Brand);

            if (request.Type != CarType.Undefined)
                cars = cars.Where(x => x.Type == request.Type);

            if (cars.Count() < request.Amount)
            {
                result = "Not Enought Cars";
            }
            else
            {
                List<Car> carsToRent = cars.Take(request.Amount).ToList();

                carsToRent.ForEach(x => {
                    x.RentedUntilDate = DateTime.Now.AddDays(request.Days);
                    RentCar(x, request.CompanyId);
                    });

                result = "Rented successfully!";
                rentalSucceded = true;
            }

            request.Status = rentalSucceded ? RentalResponseStatus.Succeded : RentalResponseStatus.Failed;
            request.StatusMessage = result;
            _rentalRequestRepository.Insert(request);
            _rentalRequestRepository.Save();

            return result;
        }

        private void ReturnCarResponseMarkAsNotFound(ReturnCarResponse response, int carId)
        {
            response.Status = ReturnCarResponseStatus.Failed.ToString();
            response.Message = $"Something went wrong, check {nameof(response.CarResults)} for more info.";
            response.CarResults.Add(new ReturnCarResult()
            {
                Message = $"Car with Id {carId} does not exist. Return request will fail.",
                Status = ReturnCarStatus.CarNotFound.ToString()
            });
        }
        private void ReturnCarResponseMarkAsNotRented(ReturnCarResponse response, int carId)
        {
            response.Status = ReturnCarResponseStatus.Failed.ToString();
            response.Message = $"Something went wrong, check {nameof(response.CarResults)} for more info.";
            response.CarResults.Add(new ReturnCarResult()
            {
                Message = $"Car with Id {carId} is not rented. Return request will fail.",
                Status = ReturnCarStatus.CarIsNotRented.ToString()
            });
        }
        private void ReturnCarResponseAddExtraCost(ReturnCarResponse response, Car car)
        {
            int extraDays = (DateTime.Now.Date - car.RentedUntilDate.Value.Date).Days;
            int extraPrice = extraDays * car.PricePerDay;

            response.CarResults.Add(new ReturnCarResult()
            {
                Message = $"Car with Id {car.Id} will be charged with extra price because is returned late. Extra price is {extraPrice}.",
                Status = ReturnCarStatus.SuccededWithExtraPrice.ToString()
            });
        }
    }
    
}
