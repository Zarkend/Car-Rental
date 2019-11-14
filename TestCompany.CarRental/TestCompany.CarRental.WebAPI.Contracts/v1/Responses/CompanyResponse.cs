using System;
using System.Collections.Generic;
using System.Text;
using TestCompany.CarRental.Domain.Enums;

namespace TestCompany.CarRental.WebAPI.Contracts.v1.Responses
{
    public class CompanyResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BonusPoints { get; set; }
    }
}
