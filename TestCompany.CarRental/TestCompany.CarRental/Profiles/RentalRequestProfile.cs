using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCompany.CarRental.Domain.Requests;
using TestCompany.CarRental.WebAPI.ApiRequests;

namespace TestCompany.CarRental.WebAPI.Profiles
{
    public class RentalRequestProfile : Profile
    {
        public RentalRequestProfile()
        {
            CreateMap<ApiRequests.RentRequest, Domain.Requests.RentRequest>();
        }
    }
}
