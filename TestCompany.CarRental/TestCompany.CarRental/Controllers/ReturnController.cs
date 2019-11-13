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
    public class ReturnController : ControllerBase
    {

        private readonly ILogger<CarsController> _logger;
        private readonly IReturnService _returnService;

        public ReturnController(ILogger<CarsController> logger, IReturnService returnService)
        {
            _logger = logger;
            _returnService = returnService;
        }

        /// <summary>
        /// Try to return a list of Cars.
        /// </summary>
        /// <returns></returns>
        [HttpPost("")]
        public async Task<ActionResult> ReturnCarsAsync(IEnumerable<int> carIds)
        {

            if (!carIds.Any())
            {
                return BadRequest($"Please provide atleast one car to return.");
            }

            ReturnCarResponse response = await _returnService.ReturnCarsAsync(carIds);

            return Ok(response);
        }
    }
}
