using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCompany.CarRental.Domain.Enums;

namespace TestCompany.CarRental.WebAPI.Contracts.v1.Requests
{ 
    public class RentRequest
    {
        public List<int> CarIds { get; set; } = new List<int>();
        public int CompanyId { get; set; }
        public int Days { get; set; }

    }

}
