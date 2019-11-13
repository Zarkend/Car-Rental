using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TestCompany.CarRental.Domain.Entities;
using TestCompany.CarRental.Domain.Entities.Responses;
using TestCompany.CarRental.Domain.Requests;

namespace TestCompany.CarRental.Domain.ServiceContracts
{
    public interface IReturnService
    {
        Task<ReturnCarResponse> ReturnCarsAsync(IEnumerable<int> carIds);
    }
}
