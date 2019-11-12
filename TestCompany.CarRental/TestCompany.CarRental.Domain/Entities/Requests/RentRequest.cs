using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TestCompany.CarRental.Domain.Entities;
using TestCompany.CarRental.Domain.Enums;

namespace TestCompany.CarRental.Domain.Requests
{
    public class RentRequest
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [NotMapped]
        public List<int> CarIds { get; set; }
        public int? CompanyId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public RentCarResponseStatus Status { get; set; }
        public string StatusMessage { get; set; }
        public int Days { get; set; }
        public Company Company { get; set; }
    }
}
