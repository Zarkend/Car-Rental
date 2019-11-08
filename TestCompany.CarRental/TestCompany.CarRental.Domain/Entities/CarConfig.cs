using System;
using System.Collections.Generic;
using System.Text;
using TestCompany.CarRental.Domain.Enums;

namespace TestCompany.CarRental.Domain.Entities
{
    public class CarConfig
    {
        public CarType CarType { get; set; }
        public PriceType PriceType { get; set; }
        public int BonusPointsPerRental { get; set; }
    }
}
