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
    [Route("[controller]")]
    public class RentalController : ControllerBase
    {

        private readonly ILogger<FleetController> _logger;
        private readonly IRentalService _rentalService;
        private readonly IFleetService _fleetService;
        private readonly IMapper _mapper;

        public RentalController(ILogger<FleetController> logger, IRentalService rentalService, IMapper mapper, IFleetService fleetService)
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
        public ActionResult RentCars(IEnumerable<RentRequest> rentalRequests)
        {
            if (!rentalRequests.Any())
            {
                return BadRequest($"Please provide atleast one RentalRequest.");
            }

            foreach(RentRequest request in rentalRequests)
            {
                _rentalService.ProcessRentalRequest(_mapper.Map<RentalRequest>(request));
            }            

            return Ok("Rented Successfuly");
        }

        /// <summary>
        /// Try to return a list of Cars.
        /// </summary>
        /// <returns></returns>
        [HttpPost("return")]
        public ActionResult ReturnCars(IEnumerable<int> carIds)
        {

            if (!carIds.Any())
            {
                return BadRequest($"Please provide atleast one car to return.");
            }

            ReturnCarResponse response = _rentalService.ReturnCars(carIds);

            return Ok(response);
        }

        /// <summary>
        /// Gets a list of the available cars to rent.
        /// </summary>
        /// <returns></returns>
        [HttpPost("availability")]
        public ActionResult<IEnumerable<Car>> PostCar()
        {
            IEnumerable<Car> cars = _fleetService.Get().Where(x => !x.Rented);
            
            if (!cars.Any())
            {
                return NotFound($"There is no cars to rent at that moment. Try again later.");
            }

            return Ok(cars);
        }




    }
}
