using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestCompany.CarRental.Domain.Entities;
using TestCompany.CarRental.Domain.InfrastructureContracts;
using TestCompany.CarRental.Domain.Requests;
using TestCompany.CarRental.Domain.UnitOfWork;
using TestCompany.CarRental.Infrastructure.DbContexts;
using TestCompany.CarRental.Infrastructure.Repositories;

namespace TestCompany.CarRental.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {

        private CarRentalContext _dbContext;
        private BaseRepository<Car> _cars;
        private BaseRepository<RentRequest> _rentalRequests;
        private BaseRepository<Company> _companies;

        public UnitOfWork(CarRentalContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IRepository<Car> Cars
        {
            get
            {
                return _cars ??
                    (_cars = new CarRepository(_dbContext));
            }
        }

        public IRepository<RentRequest> RentalRequests
        {
            get
            {
                return _rentalRequests ??
                    (_rentalRequests = new BaseRepository<RentRequest>(_dbContext));
            }
        }

        public IRepository<Company> Companies
        {
            get
            {
                return _companies ??
                    (_companies = new CompanyRepository(_dbContext));
            }
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public void Rollback()
        {
            _dbContext.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
        }
    }
}
