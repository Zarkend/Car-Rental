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
    public class ReturnService : IReturnService
    {
        private IFleetService _fleetService;
        private IUnitOfWork _unitOfWork;

        public ReturnService(IFleetService fleetService, IUnitOfWork unitOfWork)
        {
            _fleetService = fleetService;
            _unitOfWork = unitOfWork;
        }                      

        public ReturnCarResponse ReturnCars(IEnumerable<int> carIds)
        {
            ReturnCarResponse response = new ReturnCarResponse();
            List<Car> cars = _unitOfWork.Cars.Get(x => carIds.Contains(x.Id)).ToList();

            foreach (var carId in carIds)
            {
                Car car = cars.FirstOrDefault(x=> x.Id == carId);

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
                        Message = $"Car with Id {carId} returned correctly without extra cost.",
                        Status = ReturnCarStatus.Succeded.ToString()
                    });
                }

                ReturnCar(car);
               
            }

            response.Status = ReturnCarResponseStatus.Succeded.ToString();
            _unitOfWork.Commit();

            return response;
        }

        private void ReturnCar(Car car)
        {
            car.Rented = false;
            car.RentedUntilDate = null;
            car.RentedDate = null;
            car.CompanyId = null;
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
