using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestCompany.CarRental.Domain.Entities;
using TestCompany.CarRental.Domain.Enums;
using TestCompany.CarRental.Domain.Requests;
using TestCompany.CarRental.Domain.ServiceContracts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestCompany.CarRental.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class FleetController : ControllerBase
    {

        private readonly ILogger<FleetController> _logger;
        private readonly IFleetService _fleetService;

        public FleetController(ILogger<FleetController> logger, IFleetService fleetService)
        {
            _logger = logger;
            _fleetService = fleetService;
        }

        [HttpGet("{carId}")]
        public ActionResult<Car> Get(int carId)
        {
            IEnumerable<Car> cars = _fleetService.Get();

            Car car = cars.FirstOrDefault(x => x.Id == carId);

            if(car == null)
            {
                return NotFound($"There is no car with Id {carId}. Please try with another carId");
            }

            return Ok(car);
        }

        /// <summary>
        /// Get cars matching the parameters provided.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<Car>> GetCar(int? id, string? registration, CarType carType, Brand brand, bool? rented)
        {
            IEnumerable<Car> cars = _fleetService.Get();

            if (id.HasValue)
                cars = cars.Where(x => x.Id == id);

            if (!string.IsNullOrEmpty(registration))
                cars = cars.Where(x => x.Registration == registration);
            
            if (carType != CarType.Undefined)
                cars = cars.Where(x => x.Type == carType);

            if (brand != Brand.Undefined)
                cars = cars.Where(x => x.Brand == brand);
            if(rented.HasValue)
                cars = cars.Where(x => x.Rented == rented);

            if (!cars.Any())
            {
                return NotFound(new NotFoundObjectResult($"There is no cars wich matches all the parameters passed. Please try again."));
            }

            return Ok(cars);
        }

    }
}
