using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TestCompany.CarRental.WebAPI.Contracts.v1.Requests;
using TestCompany.CarRental.WebAPI.Contracts.v1.Responses;
using Xunit;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestCompany.CarRental.Infrastructure.DbContexts;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using TestCompany.CarRental.WebAPI.ApiRequests;

namespace TestCompany.CarRental.WebAPI.IntegrationTests
{
    public class CarControllerTest : BaseTest
    {

        [Fact]
        public async Task GetAll_WithoutAnyCars_ReturnsEmptyResponse()
        {
            //Arrange


            //Act
            var response = await _client.GetAsync("/api/v1/Cars" + ApiRoutes.Cars.GetAll);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var content = await response.Content.ReadAsAsync<Response<List<CarResponse>>>();
            content.Data.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAll_WithOneCar_ReturnsOneCar()
        {
            //Arrange
            CarResponse createdCar = await CreateCarAsync(new CreateCarRequest()
            {
                Brand = Domain.Enums.Brand.Audi,
                Model ="A4",
                Registration = "1234567891",
                Type = Domain.Enums.CarType.SUV
            });

            //Act
            var response = await _client.GetAsync("/api/v1/Cars" + ApiRoutes.Cars.GetAll);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var content = await response.Content.ReadAsAsync<Response<List<CarResponse>>>();
            content.Data.First().Id.Should().Be(createdCar.Id);
            content.Data.First().Brand.Should().Be(createdCar.Brand);
            content.Data.First().Model.Should().Be(createdCar.Model);
            content.Data.First().Registration.Should().Be(createdCar.Registration);
            content.Data.First().Type.Should().Be(createdCar.Type);
        }


    }
}
