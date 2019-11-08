using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCompany.CarRental.Domain.Entities;
using TestCompany.CarRental.Domain.Enums;

namespace TestCompany.CarRental.Domain.Requests
{
    public class RentalRequest
    {
        public CarType Type { get; set; }
        public Brand Brand { get; set; } 
        public int Amount { get; set; }
        public int CompanyId { get; set; }
    }
}
