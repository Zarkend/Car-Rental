using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TestCompany.CarRental.Domain.Entities;

namespace TestCompany.CarRental.Domain.ServiceContracts
{
    public interface IFleetService
    {
        IEnumerable<Car> Get(Expression<Func<Car, bool>> filter = null);
    }
}
