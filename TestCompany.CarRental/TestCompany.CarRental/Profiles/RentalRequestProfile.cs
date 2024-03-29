﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCompany.CarRental.Domain.Requests;

namespace TestCompany.CarRental.WebAPI.Profiles
{
    public class RentalRequestProfile : Profile
    {
        public RentalRequestProfile()
        {
            CreateMap<Contracts.v1.Requests.RentRequest, Domain.Requests.RentRequest>();
            CreateMap<Domain.Entities.Responses.RentRequestResponse, Contracts.v1.Responses.RentRequestResponse>()
                .ForMember(dest => dest.Status, opts => opts.MapFrom(src => src.Status.ToString()));
            CreateMap<Domain.Entities.Responses.RentCarResponse, Contracts.v1.Responses.RentCarResponse>()
                .ForMember(dest => dest.Status, opts => opts.MapFrom(src => src.Status.ToString()));
        }
    }
}
