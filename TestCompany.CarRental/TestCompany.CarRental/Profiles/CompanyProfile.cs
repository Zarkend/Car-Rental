using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCompany.CarRental.Domain.Entities;
using TestCompany.CarRental.WebAPI.ApiRequests;
using TestCompany.CarRental.WebAPI.Contracts.v1.Responses;

namespace TestCompany.CarRental.WebAPI.Profiles
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<Company, CompanyResponse>();
            CreateMap<CreateCompanyRequest, Company>();
        }
    }
}
