using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
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
        private ICarService _fleetService;
        private IUnitOfWork _unitOfWork;

        public RentalService(ICarService fleetService, IUnitOfWork unitOfWork)
        {
            _fleetService = fleetService;
            _unitOfWork = unitOfWork;
        }

        public async Task<RentRequestResponse> CreateRentAsync(RentRequest request)
        {
            RentRequestResponse response = new RentRequestResponse();

            IEnumerable<Car> cars = await _fleetService.GetAsync(x => request.CarIds.Contains(x.Id));
            IEnumerable<Car> alreadyRentedCars = cars.Where(x => x.Rented);
            request.Company = await _unitOfWork.Companies.GetByIDAsync(request.CompanyId);

            if(request.Company == null)
            {
                response.Status = RentCarResponseStatus.Failed;
                response.Message = $"Company with Id {request.CompanyId} does not exists.";
                return response;
            }
            try
            {
                if (alreadyRentedCars.Any())
                {
                    request.Status = RentCarResponseStatus.Failed;
                    request.StatusMessage = $"Some of the cars are already rented. {Environment.NewLine} {string.Join(',', alreadyRentedCars.Select(x => x.Id + Environment.NewLine))}";

                    response.Status = RentCarResponseStatus.Failed;
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
                    response.Status = RentCarResponseStatus.Failed;
                    response.Message = $"Please provide atleast one(1) carId in the rentalRequest.";
                }
            }
            catch(Exception ex)
            {
                request.Status = RentCarResponseStatus.Failed;
                request.StatusMessage = $"Some error ocurred while processing the rental request.Rollback executed {Environment.NewLine}{ex.ToString()}";
                response.Status = RentCarResponseStatus.Failed;
                response.Message = $"Some error ocurred while processing the rental request {ex.Message}";
                await _unitOfWork.RentalRequests.InsertAsync(request);
                _unitOfWork.Rollback();
            }
            finally
            {
                await _unitOfWork.RentalRequests.InsertAsync(request);
                _unitOfWork.Commit();
            }
            return response;
        }

        private RentRequestResponse RentCars(IEnumerable<Car> cars, RentRequest request)
        {
            RentRequestResponse response = new RentRequestResponse();

            foreach (Car car in cars)
            {
                RentCar(car, request.CompanyId.Value,request.Days);

                request.Company.BonusPoints += car.BonusPointsPerRental;
                
                response.CarResults.Add(new RentCarResponse()
                {
                    Status = RentCarStatus.Succeded,
                    Message = $"CarId {car.Id} rented successfully by companyId {request.CompanyId}.",
                    RentPrice = car.PricePerDay * request.Days
                });
            }

            response.Status = RentCarResponseStatus.Succeded;
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
                
    }
    
}
