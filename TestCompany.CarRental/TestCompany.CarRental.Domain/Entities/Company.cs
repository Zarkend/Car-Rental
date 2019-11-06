using System;
using System.Collections.Generic;
using System.Text;

namespace TestCompany.CarRental.Domain.Entities
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BonusPoints { get; set; }
    }
}
