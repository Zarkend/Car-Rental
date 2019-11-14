using System;
using System.Collections.Generic;
using System.Text;
using TestCompany.CarRental.Domain.Enums;

namespace TestCompany.CarRental.WebAPI.Contracts.v1.Responses
{
    public class RentCarResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public int RentPrice { get; set; }
    }
}
