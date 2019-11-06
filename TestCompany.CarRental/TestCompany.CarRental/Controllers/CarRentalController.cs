using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestCompany.CarRental.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarRentalController : ControllerBase
    {

        private readonly ILogger<CarRentalController> _logger;

        public CarRentalController(ILogger<CarRentalController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// DO SOMETHING
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Get()
        {
            return Ok("hola");
        }

    }
}
