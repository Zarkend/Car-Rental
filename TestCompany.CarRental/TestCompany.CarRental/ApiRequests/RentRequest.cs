using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCompany.CarRental.Domain.Enums;

namespace TestCompany.CarRental.WebAPI.ApiRequests
{
    public class RentRequest
    {
        public List<int> CarIds { get; set; }
        public int CompanyId { get; set; }
        public int Days { get; set; }

    }

}
