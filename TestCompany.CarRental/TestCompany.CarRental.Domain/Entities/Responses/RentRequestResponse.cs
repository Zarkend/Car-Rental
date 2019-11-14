using System;
using System.Collections.Generic;
using System.Text;
using TestCompany.CarRental.Domain.Enums;

namespace TestCompany.CarRental.Domain.Entities.Responses
{
    public class RentRequestResponse
    {
        public RentCarResponseStatus Status { get; set; }
        public string Message { get; set; }
        public List<RentCarResponse> CarResults { get; set; } = new List<RentCarResponse>();
    }
}
