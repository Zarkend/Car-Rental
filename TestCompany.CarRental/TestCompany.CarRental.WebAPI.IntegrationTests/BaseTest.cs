using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TestCompany.CarRental.WebAPI.ApiRequests;
using TestCompany.CarRental.WebAPI.Contracts.v1.Requests;
using TestCompany.CarRental.WebAPI.Contracts.v1.Responses;

namespace TestCompany.CarRental.WebAPI.IntegrationTests
{
    public class BaseTest
    {
        protected readonly HttpClient _client;
        public BaseTest()
        {
            var builder = new WebHostBuilder().UseEnvironment("Testing").UseStartup<Startup>();


            var server = new TestServer(builder);
            _client = server.CreateClient();

            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html"));
        }


        protected async Task<CarResponse> CreateCarAsync(CreateCarRequest request)
        {
            var response = await _client.PostAsJsonAsync("/api/v1/Cars" + ApiRoutes.Cars.Create, request);
            return (await response.Content.ReadAsAsync<Response<CarResponse>>()).Data;
        }
    }
}
