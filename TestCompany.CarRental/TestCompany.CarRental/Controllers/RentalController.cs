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
    public class RentalController : ControllerBase
    {

        private readonly ILogger<FleetController> _logger;
        private readonly IRentalService _rentalService;

        public RentalController(ILogger<FleetController> logger, IRentalService rentalService)
        {
            _logger = logger;
            _rentalService = rentalService;
        }

        /// <summary>
        /// Try to rent cars of type. If available return Okey.
        /// </summary>
        /// <returns></returns>
        [HttpPost("rent")]
        public ActionResult RentCars(IEnumerable<RentalRequest> rentalRequests)
        {
            IEnumerable<Car> cars = _rentalService.GetCars();
            List<Car> rentedCars = new List<Car>();

            if (!rentalRequests.Any())
            {
                return NotFound($"Please provide atleast one RentalRequest.");
            }

            foreach(RentalRequest request in rentalRequests)
            {
                _rentalService.ProcessRentalRequest(request);
            }
            

            return Ok(cars);
        }       




    }
}
