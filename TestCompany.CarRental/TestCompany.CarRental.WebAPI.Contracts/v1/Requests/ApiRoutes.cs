using System;
using System.Collections.Generic;
using System.Text;

namespace TestCompany.CarRental.WebAPI.Contracts.v1.Requests
{
    public static class ApiRoutes
    {
        public static class Cars
        {
            public const string GetAll = ""; 
            public const string Update = "{carId}";

            public const string Delete = "{carId}";

            public const string Get = "{carId}";

            public const string Create = "";
        }

        public static class Companies
        {
            public const string Update = "/companies/{companyId}";

            public const string Delete = "/companies/{companyId}";

            public const string Get = "/companies/{companyId}";

            public const string Create = "/companies";
        }

        public static class Rents
        {
            public const string Create = "/rents";
        }

        public static class Returns
        {
            public const string Create = "/rents";
        }
    }
}
