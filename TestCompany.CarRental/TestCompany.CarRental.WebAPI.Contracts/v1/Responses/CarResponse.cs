using System;
using System.Collections.Generic;
using System.Text;
using TestCompany.CarRental.Domain.Enums;

namespace TestCompany.CarRental.WebAPI.Contracts.v1.Responses
{
    public class CarResponse
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Registration { get; set; }
        public Brand Brand { get; set; }
        public CarType Type { get; set; }
        public bool Rented { get; set; }
        public DateTime? RentedDate { get; set; }
        public int? CompanyId { get; set; }
        public int PricePerDay { get; set; }
        public int BonusPointsPerRental { get; set; }
        public DateTime? RentedUntilDate { get; set; }
    }
}
