using System;
using System.Collections.Generic;
using System.Text;
using TestCompany.CarRental.Domain.Enums;

namespace TestCompany.CarRental.Domain.Entities
{
    public class Car
    {
        public int Id { get; set; }
        public string Registration { get; set; }
        public CarType Type { get; set; } 
        public int Price { get; set; }
        public bool Rented { get; set; }

    }
}
