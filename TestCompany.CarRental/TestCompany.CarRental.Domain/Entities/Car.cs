using System;
using System.Collections.Generic;
using System.Text;
using TestCompany.CarRental.Domain.Enums;

namespace TestCompany.CarRental.Domain.Entities
{
    public class Car
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Registration { get; set; }
        public Brand Brand { get; set; }
        public CarType Type { get; set; } 
        public bool Rented { get; set; }
        public DateTime RentedDate { get; set; }
        public Company RentedCompany { get; set; }


    }
}
