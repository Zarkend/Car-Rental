using System;
using System.Collections.Generic;
using System.Text;
using TestCompany.CarRental.Domain.Enums;

namespace TestCompany.CarRental.Domain.Entities.Responses
{
    public class ReturnCarResult
    {
        public string Status { get; set; }
        public string Message { get; set; }
    }
}
