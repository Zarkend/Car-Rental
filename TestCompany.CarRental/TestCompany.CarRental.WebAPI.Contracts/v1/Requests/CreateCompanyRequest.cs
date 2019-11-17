using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCompany.CarRental.Domain.Enums;

namespace TestCompany.CarRental.WebAPI.Contracts.v1.Requests
{ 
    public class CreateCompanyRequest
    {
        public string Name { get; set; }
    }
}
