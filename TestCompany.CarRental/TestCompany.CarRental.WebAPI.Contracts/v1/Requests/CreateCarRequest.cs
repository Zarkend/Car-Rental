using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCompany.CarRental.Domain.Enums;

namespace TestCompany.CarRental.WebAPI.Contracts.v1.Requests
{
    public class CreateCarRequest
    {
        public string Model { get; set; }
        public string Registration { get; set; }
        public Brand Brand { get; set; }
        public CarType Type { get; set; }
    }
}
