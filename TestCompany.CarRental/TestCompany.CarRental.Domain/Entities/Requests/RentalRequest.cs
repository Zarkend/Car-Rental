﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TestCompany.CarRental.Domain.Entities;
using TestCompany.CarRental.Domain.Enums;

namespace TestCompany.CarRental.Domain.Requests
{
    public class RentalRequest
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public CarType Type { get; set; }
        public Brand Brand { get; set; } 
        public int Amount { get; set; }
        public int CompanyId { get; set; }
    }
}
