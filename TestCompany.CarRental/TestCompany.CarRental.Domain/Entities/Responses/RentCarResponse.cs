using System;
using System.Collections.Generic;
using System.Text;

namespace TestCompany.CarRental.Domain.Entities.Responses
{
    public class RentCarResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public List<RentCarResult> CarResults { get; set; } = new List<RentCarResult>();
    }
}
