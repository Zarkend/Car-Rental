using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCompany.CarRental.WebAPI.Contracts.v1.Requests;

namespace TestCompany.CarRental.WebAPI.Services
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;

        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }

        public Uri GetCarUri(string carId)
        {
            return new Uri(_baseUri + ApiRoutes.Cars.Get.Replace("{carId}", carId));
        }
        public Uri GetCompanyUri(string companyId)
        {
            return new Uri(_baseUri + ApiRoutes.Companies.Get.Replace("{companyId}", companyId));
        }
    }
}
