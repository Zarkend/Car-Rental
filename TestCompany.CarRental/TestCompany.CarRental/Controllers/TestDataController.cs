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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestCompany.CarRental.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestDataController : ControllerBase
    {
        private readonly ILogger<TestDataController> _logger;
        private readonly IRepository<Car> _carRepository;
        private readonly IRepository<Company> _companyRepository;

        public TestDataController(ILogger<TestDataController> logger, IRepository<Car> carRepository, IRepository<Company> companyRepository)
        {
            _logger = logger;
            _carRepository = carRepository;
            _companyRepository = companyRepository;
        }

        /// <summary>
        /// Fills the sample data.
        /// </summary>
        /// <returns></returns>
        [HttpPost("FillSampleData")]
        public ActionResult<IEnumerable<Car>> PostData()
        {
            _carRepository.FillTestData();
            _companyRepository.FillTestData();
            return Ok();
        }

    }
}
