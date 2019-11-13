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
    public class RentController : ControllerBase
    {

        private readonly ILogger<CarsController> _logger;
        private readonly IRentalService _rentalService;
        private readonly IFleetService _fleetService;
        private readonly IMapper _mapper;

        public RentController(ILogger<CarsController> logger, IRentalService rentalService, IMapper mapper, IFleetService fleetService)
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
        public async Task<ActionResult> RentCarsAsync(IEnumerable<WebAPI.ApiRequests.RentRequest> rentalRequests)
        {
            List<RentCarResponse> responses = new List<RentCarResponse>();
            if (!rentalRequests.Any())
            {
                return BadRequest(new BadRequestObjectResult($"Please provide atleast one RentalRequest."));
            }

            foreach(WebAPI.ApiRequests.RentRequest request in rentalRequests)
            {
                RentCarResponse response = await _rentalService.ProcessRentalRequestAsync(_mapper.Map<Domain.Requests.RentRequest>(request));
                responses.Add(response);
            }            

            return Ok(responses);
        }


        /// <summary>
        /// Gets a list of the available cars to rent.
        /// </summary>
        /// <returns></returns>
        [HttpGet("availableCars")]
        public async Task<ActionResult<IEnumerable<Car>>> GetAvailableCarsAsync(CarType carType, Brand brand)
        {
            IEnumerable<Car> cars = await _fleetService.GetAsync();

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
        public async Task<ActionResult<bool>> GetAvailableCar(int carId)
        {
            Car car = (await _fleetService.GetAsync()).FirstOrDefault(x => x.Id == carId);

            if (car == null)
            {
                return NotFound(new NotFoundObjectResult($"There is no car with Id {carId}."));
            }

            return Ok(car.Rented);
        }




    }
}
