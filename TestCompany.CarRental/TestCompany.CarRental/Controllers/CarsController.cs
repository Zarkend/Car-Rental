using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestCompany.CarRental.Domain.Entities;
using TestCompany.CarRental.Domain.Enums;
using TestCompany.CarRental.Domain.Requests;
using TestCompany.CarRental.Domain.ServiceContracts;
using TestCompany.CarRental.WebAPI.Contracts.v1.Requests;
using TestCompany.CarRental.WebAPI.Contracts.v1.Responses;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestCompany.CarRental.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly ILogger<CarsController> _logger;
        private readonly IFleetService _fleetService;
        private readonly IMapper _mapper;

        public CarsController(ILogger<CarsController> logger, IFleetService fleetService, IMapper mapper)
        {
            _logger = logger;
            _fleetService = fleetService;
            _mapper = mapper;
        }

        [HttpGet(ApiRoutes.Cars.Get)]
        public async Task<IActionResult> GetAsync([FromRoute]int carId)
        {
            IEnumerable<Car> cars = await _fleetService.GetAsync();

            Car car = cars.FirstOrDefault(x => x.Id == carId);

            if(car == null)
            {
                return NotFound($"There is no car with Id {carId}. Please try with another carId");
            }

            return Ok(car);
        }

        [HttpPut(ApiRoutes.Cars.Update)]
        public async Task<IActionResult> UpdateAsync([FromRoute]int carId, [FromBody] UpdateCarRequest request)
        {

            Car car = (await _fleetService.GetAsync(x => x.Id == carId)).FirstOrDefault();

            if (car == null)
            {
                return NotFound(new NotFoundObjectResult($"There is no car with Id {carId}. Please try with another carId"));
            }

            car.Registration = request.Registration;
            car.BonusPointsPerRental = request.BonusPointsPerRental;
            car.PricePerDay = request.PricePerDay;

            var updated = await _fleetService.UpdateCarAsync(car);

            if (!updated)
                return NotFound(new NotFoundObjectResult($"Car was not updated."));

            return Ok(new Response<CarResponse>(_mapper.Map<CarResponse>(car)));
        }

        [HttpDelete(ApiRoutes.Cars.Delete)]
        public async Task<IActionResult> DeleteAsync([FromRoute]int carId)
        {
            Car car = (await _fleetService.GetAsync(x => x.Id == carId)).FirstOrDefault();

            if (car == null)
            {
                return NotFound(new NotFoundObjectResult($"There is no car with Id {carId}. Please try with another carId"));
            }

            var deleted = await _fleetService.DeleteCarAsync(car);

            if (!deleted)
                return NotFound(new NotFoundObjectResult($"Car was not deleted."));


            return NoContent();
        }

        /// <summary>
        /// Get cars matching the parameters provided.
        /// </summary>
        /// <returns></returns>
        [HttpGet(ApiRoutes.Cars.GetAll)]
        public async Task<ActionResult<IEnumerable<Car>>> GetCarAsync([FromQuery]int? id, [FromQuery] string? registration, [FromQuery] CarType carType, [FromQuery] Brand brand, [FromQuery] bool? rented)
        {
            IEnumerable<Car> cars = await _fleetService.GetAsync();

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
