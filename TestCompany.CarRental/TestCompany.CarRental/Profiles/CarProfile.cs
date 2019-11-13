using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCompany.CarRental.Domain.Entities;
using TestCompany.CarRental.WebAPI.Contracts.v1.Responses;

namespace TestCompany.CarRental.WebAPI.Profiles
{
    public class CarProfile : Profile
    {
        public CarProfile()
        {
            CreateMap<Car, CarResponse>();
        }
    }
}
