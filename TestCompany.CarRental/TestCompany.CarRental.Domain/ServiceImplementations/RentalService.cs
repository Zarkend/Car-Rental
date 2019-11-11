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
using TestCompany.CarRental.Domain.UnitOfWork;

namespace TestCompany.CarRental.Domain.ServiceImplementations
{
    public class RentalService : IRentalService
    {
        private IFleetService _fleetService;
        private IUnitOfWork _unitOfWork;

        public RentalService(IFleetService fleetService, IUnitOfWork unitOfWork)
        {
            _fleetService = fleetService;
            _unitOfWork = unitOfWork;
        }

        public string ProcessRentalRequest(RentalRequest request)
        {
            string result = "";

            Car car = _fleetService.Get(x => !x.Rented).FirstOrDefault(x=> x.Id == request.CarId);

            RentCars(new List<Car>() { car }, request);


            return result;
        }
        

        public ReturnCarResponse ReturnCars(IEnumerable<int> carIds)
        {
            ReturnCarResponse response = new ReturnCarResponse();
            response.Status = ReturnCarResponseStatus.Succeded.ToString();

            foreach (var carId in carIds)
            {
                Car car = _unitOfWork.Cars.Get(x=> x.Id == carId).FirstOrDefault();

                if(car == null)
                {
                    ReturnCarResponseMarkAsNotFound(response,carId);
                    _unitOfWork.Rollback();
                    return response;
                }

                if (!car.Rented) {
                    ReturnCarResponseMarkAsNotRented(response, carId);
                    _unitOfWork.Rollback();
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
                _unitOfWork.Cars.Update(car);
                
            }

            _unitOfWork.Commit();

            return response;
        }


        private void RentCars(List<Car> cars, RentalRequest request)
        {
            cars.ForEach(x => {
                x.RentedUntilDate = DateTime.Now.AddDays(request.Days);
                RentCar(x, request.CompanyId);
            });

            request.Status = RentalResponseStatus.Succeded;
            request.StatusMessage = "Rental Succeded!";
            _unitOfWork.RentalRequests.Insert(request);
            _unitOfWork.Commit();

        }

        private void RentCar(Car car, int companyId)
        {
            car.CompanyId = companyId;
            car.Rented = true;
            car.RentedDate = DateTime.Now;
            _unitOfWork.Cars.Update(car);
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
