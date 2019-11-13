using System;
using System.Collections.Generic;
using System.Text;

namespace TestCompany.CarRental.WebAPI.Contracts.v1.Requests
{
    public class UpdateCarRequest
    {
        public string Registration { get; set; }
        public int PricePerDay { get; set; }
        public int BonusPointsPerRental { get; set; }
    }
}
