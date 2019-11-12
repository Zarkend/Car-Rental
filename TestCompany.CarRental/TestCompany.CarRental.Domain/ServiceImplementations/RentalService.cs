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

        public RentCarResponse ProcessRentalRequest(RentRequest request)
        {
            RentCarResponse response = new RentCarResponse();

            IEnumerable<Car> cars = _fleetService.Get(x => request.CarIds.Contains(x.Id));
            IEnumerable<Car> alreadyRentedCars = cars.Where(x => x.Rented);
            request.Company = _unitOfWork.Companies.GetByID(request.CompanyId);

            if(request.Company == null)
            {
                response.Status = RentCarResponseStatus.Failed.ToString();
                response.Message = $"Company with Id {request.CompanyId} does not exists.";
                return response;
            }
            try
            {
                if (alreadyRentedCars.Any())
                {
                    request.Status = RentCarResponseStatus.Failed;
                    request.StatusMessage = $"Some of the cars are already rented. {Environment.NewLine} {string.Join(',', alreadyRentedCars.Select(x => x.Id + Environment.NewLine))}";

                    response.Status = RentCarResponseStatus.Failed.ToString();
                    response.Message = $"Cars provided cannot be rented. The following cars are already rented, remove them from the request: {Environment.NewLine} {string.Join(Environment.NewLine, alreadyRentedCars.Select(x => x.Id + Environment.NewLine))}";
                }
                else if (cars.Any())
                {
                    response = RentCars(cars, request);
                }
                else
                {
                    request.Status = RentCarResponseStatus.Failed;
                    request.StatusMessage = $"0 carIds provided.";
                    response.Status = RentCarResponseStatus.Failed.ToString();
                    response.Message = $"Please provide atleast one(1) carId in the rentalRequest.";
                }
            }
            catch(Exception ex)
            {
                request.Status = RentCarResponseStatus.Failed;
                request.StatusMessage = $"Some error ocurred while processing the rental request.Rollback executed {Environment.NewLine}{ex.ToString()}";
                response.Status = RentCarResponseStatus.Failed.ToString();
                response.Message = $"Some error ocurred while processing the rental request {ex.Message}";
                _unitOfWork.RentalRequests.Insert(request);
                _unitOfWork.Rollback();
            }
            finally
            {
                _unitOfWork.RentalRequests.Insert(request);
                _unitOfWork.Commit();
            }
            return response;
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


        private RentCarResponse RentCars(IEnumerable<Car> cars, RentRequest request)
        {
            RentCarResponse response = new RentCarResponse();

            foreach (Car car in cars)
            {
                RentCar(car, request.CompanyId.Value,request.Days);

                request.Company.BonusPoints += car.BonusPointsPerRental;
                
                response.CarResults.Add(new RentCarResult()
                {
                    Status = RentCarStatus.Succeded.ToString(),
                    Message = $"CarId {car.Id} rented successfully by companyId {request.CompanyId}.",
                    RentPrice = car.PricePerDay * request.Days
                });
            }

            response.Status = RentCarResponseStatus.Succeded.ToString();
            request.Status = RentCarResponseStatus.Succeded;
            request.StatusMessage = "Rental request Succeded!";
            request.Company.UpdatedDate = DateTime.Now;
            _unitOfWork.Companies.Update(request.Company);
            return response;
        }

        private void RentCar(Car car, int companyId, int days)
        {
            car.RentedUntilDate = DateTime.Now.AddDays(days);
            car.CompanyId = companyId;
            car.Rented = true;
            car.RentedDate = DateTime.Now;
            _unitOfWork.Cars.Update(car);
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
