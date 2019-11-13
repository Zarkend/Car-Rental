using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestCompany.CarRental.Domain.Entities;
using TestCompany.CarRental.Domain.Enums;
using TestCompany.CarRental.Domain.InfrastructureContracts;
using TestCompany.CarRental.Domain.Requests;
using TestCompany.CarRental.Domain.ServiceContracts;
using TestCompany.CarRental.Domain.UnitOfWork;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestCompany.CarRental.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestDataController : ControllerBase
    {
        private readonly ILogger<TestDataController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public TestDataController(ILogger<TestDataController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Fills the sample data.
        /// </summary>
        /// <returns></returns>
        [HttpPost("FillSampleData")]
        public async Task<ActionResult<IEnumerable<Car>>> PostDataAsync()
        {
            await _unitOfWork.Cars.FillTestDataAsync();
            await _unitOfWork.Companies.FillTestDataAsync();
            return Ok();
        }

    }
}
