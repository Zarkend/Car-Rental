using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestCompany.CarRental.Domain.Entities;
using TestCompany.CarRental.Domain.Entities.Responses;
using TestCompany.CarRental.Domain.Enums;
using TestCompany.CarRental.Domain.Requests;
using TestCompany.CarRental.Domain.ServiceContracts;
using TestCompany.CarRental.WebAPI.ApiRequests;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestCompany.CarRental.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RentController : ControllerBase
    {

        private readonly ILogger<FleetController> _logger;
        private readonly IRentalService _rentalService;
        private readonly IFleetService _fleetService;
        private readonly IMapper _mapper;

        public RentController(ILogger<FleetController> logger, IRentalService rentalService, IMapper mapper, IFleetService fleetService)
        {
            _logger = logger;
            _rentalService = rentalService;
            _mapper = mapper;
            _fleetService = fleetService;
        }

        /// <summary>
        /// Process rental requests.
        /// </summary>
        /// <returns></returns>
        [HttpPost("request")]
        public ActionResult RentCars(IEnumerable<WebAPI.ApiRequests.RentRequest> rentalRequests)
        {
            List<RentCarResponse> responses = new List<RentCarResponse>();
            if (!rentalRequests.Any())
            {
                return BadRequest(new BadRequestObjectResult($"Please provide atleast one RentalRequest."));
            }

            foreach(WebAPI.ApiRequests.RentRequest request in rentalRequests)
            {
                RentCarResponse response = _rentalService.ProcessRentalRequest(_mapper.Map<Domain.Requests.RentRequest>(request));
                responses.Add(response);
            }            

            return Ok(responses);
        }


        /// <summary>
        /// Gets a list of the available cars to rent.
        /// </summary>
        /// <returns></returns>
        [HttpGet("availableCars")]
        public ActionResult<IEnumerable<Car>> GetAvailableCars(CarType carType, Brand brand)
        {
            IEnumerable<Car> cars = _fleetService.Get();

            cars = cars.Where(x => !x.Rented);

            if (carType != CarType.Undefined)
                cars = cars.Where(x => x.Type == carType);

            if (brand != Brand.Undefined)
                cars = cars.Where(x => x.Brand == brand);

            if (!cars.Any())
            {
                return NotFound(new NotFoundObjectResult($"There is no cars wich matches all the parameters passed. Please try again."));
            }

            return Ok(cars);
        }

        /// <summary>
        /// Get the available status of a car. True if the car  is available, false otherwise.
        /// </summary>
        /// <returns></returns>
        [HttpGet("availableCars/{carId}")]
        public ActionResult<bool> GetAvailableCar(int carId)
        {
            Car car = _fleetService.Get().FirstOrDefault(x => x.Id == carId);

            if (car == null)
            {
                return NotFound(new NotFoundObjectResult($"There is no car with Id {carId}."));
            }

            return Ok(car.Rented);
        }




    }
}
