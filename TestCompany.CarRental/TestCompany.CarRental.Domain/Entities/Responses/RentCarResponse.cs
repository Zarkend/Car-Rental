﻿using System;
using System.Collections.Generic;
using System.Text;
using TestCompany.CarRental.Domain.Enums;

namespace TestCompany.CarRental.Domain.Entities.Responses
{
    public class RentCarResponse
    {
        public RentCarStatus Status { get; set; }
        public string Message { get; set; }
        public int RentPrice { get; set; }
    }
}
