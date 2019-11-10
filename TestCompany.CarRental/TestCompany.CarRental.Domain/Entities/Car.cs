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
        public DateTime? RentedDate { get; set; } = DateTime.Now;
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public DateTime CreatedDate { get; set; } 



    }
}
