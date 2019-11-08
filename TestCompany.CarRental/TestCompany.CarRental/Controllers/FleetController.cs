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
    [Route("[controller]")]
    public class FleetController : ControllerBase
    {

        private readonly ILogger<FleetController> _logger;
        private readonly IRentalService _rentalService;

        public FleetController(ILogger<FleetController> logger, IRentalService rentalService)
        {
            _logger = logger;
            _rentalService = rentalService;
        }

        /// <summary>
        /// Gets one car with the registration passed as parameter.
        /// </summary>
        /// <returns></returns>
        [HttpGet("cars/{registration}")]
        public ActionResult<Car> Get(string registration)
        {
            IEnumerable<Car> cars = _rentalService.GetCars();

            Car car = cars.Where(x => x.Registration  == registration).FirstOrDefault();

            if(car == null)
            {
                return NotFound($"There is no car with registration {registration}. Please try with another registration");
            }

            return Ok(car);
        }

        /// <summary>
        /// Gets one car with the registration passed as parameter.
        /// </summary>
        /// <returns></returns>
        [HttpPost("cars")]
        public ActionResult<IEnumerable<Car>> PostCar(CarType carType)
        {
            IEnumerable<Car> cars = _rentalService.GetCars();


            if (carType != CarType.Undefined)
                cars = cars.Where(x => x.Type == carType);

            if (!cars.Any())
            {
                return NotFound($"There is no cars wich matches all the parameters passed. Please try again.");
            }

            return Ok(cars);
        }

    }
}
