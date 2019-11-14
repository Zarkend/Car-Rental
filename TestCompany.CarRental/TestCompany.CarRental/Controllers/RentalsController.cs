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
using TestCompany.CarRental.WebAPI.ApiRequests;
using TestCompany.CarRental.WebAPI.Contracts.v1.Requests;
using TestCompany.CarRental.WebAPI.Contracts.v1.Responses;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestCompany.CarRental.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RentalsController : ControllerBase
    {

        private readonly ILogger<RentalsController> _logger;
        private readonly IRentalService _rentalService;
        private readonly IMapper _mapper;

        public RentalsController(ILogger<RentalsController> logger, IRentalService rentalService, IMapper mapper)
        {
            _logger = logger;
            _rentalService = rentalService;
            _mapper = mapper;
        }

        /// <summary>
        /// Process rental requests.
        /// </summary>
        /// <returns></returns>
        [HttpPost(ApiRoutes.Rents.Create)]
        public async Task<IActionResult> CreateAsync([FromBody]IEnumerable<WebAPI.ApiRequests.RentRequest> rentRequests)
        {
            List<RentRequestResponse> responses = new List<RentRequestResponse>();
            if (!rentRequests.Any())
            {
                return BadRequest(new BadRequestObjectResult($"Please provide atleast one RentalRequest."));
            }

            foreach(WebAPI.ApiRequests.RentRequest request in rentRequests)
            {
                RentRequestResponse response = _mapper.Map<RentRequestResponse>(await _rentalService.CreateRentAsync(_mapper.Map<Domain.Requests.RentRequest>(request)));
                responses.Add(response);
            }            

            return Ok(new Response<List<RentRequestResponse>>(responses));
        }
    }
}
