using System;
using System.Collections.Generic;
using System.Text;

namespace TestCompany.CarRental.Domain.Entities.Responses
{
    public class RentCarResult
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public int RentPrice { get; set; }
    }
}
