using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCompany.CarRental.WebAPI.Services
{
    public interface IUriService
    {
        Uri GetCarUri(string carId);
    }
}
