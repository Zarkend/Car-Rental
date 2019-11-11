using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TestCompany.CarRental.Domain.Enums;

namespace TestCompany.CarRental.Domain.Entities
{
    public class Car
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Model { get; set; }
        public string Registration { get; set; }
        public Brand Brand { get; set; }
        public CarType Type { get; set; } 
        public bool Rented { get; set; }
        public DateTime? RentedDate { get; set; }
        public int? CompanyId { get; set; }
        public PriceType PriceType { get; set; }
        public int PricePerDay { get; set; }
        public int BonusPointsPerRental { get; set; }
        public DateTime CreatedDate { get; set; }
        public Company Company { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime? RentedUntilDate { get; set; }



    }
}
