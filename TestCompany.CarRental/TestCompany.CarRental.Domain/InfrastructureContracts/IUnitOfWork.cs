using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestCompany.CarRental.Domain.Entities;
using TestCompany.CarRental.Domain.InfrastructureContracts;
using TestCompany.CarRental.Domain.Requests;

namespace TestCompany.CarRental.Domain.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<Car> Cars { get; }
        IRepository<RentRequest> RentalRequests { get; }
        IRepository<Company> Companies { get; }
        void Commit();
        Task<int> CommitAsync();
        void Rollback();
    }
}
