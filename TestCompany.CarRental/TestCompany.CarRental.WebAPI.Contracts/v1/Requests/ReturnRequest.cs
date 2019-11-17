using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCompany.CarRental.WebAPI.Contracts.v1.Requests
{
    public class ReturnRequest
    {
        public List<int> CarIds { get; set; } = new List<int>();
    }
}
