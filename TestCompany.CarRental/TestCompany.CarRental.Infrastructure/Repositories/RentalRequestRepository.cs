using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TestCompany.CarRental.Domain.Entities;
using TestCompany.CarRental.Domain.Enums;
using TestCompany.CarRental.Domain.InfrastructureContracts;
using TestCompany.CarRental.Domain.Requests;
using TestCompany.CarRental.Infrastructure.DbContexts;

namespace TestCompany.CarRental.Infrastructure.Repositories
{
    public class RentalRequestRepository : IRepository<RentalRequest>
    {
        private CarRentalContext _context;

        public RentalRequestRepository(CarRentalContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
            GC.SuppressFinalize(this);
        }

        public IQueryable<RentalRequest> Query(Expression<Func<RentalRequest, bool>> filter)
        {
            return _context.RentalRequest.Where(filter);
        }

        public RentalRequest GetById(int id)
        {
            return _context.RentalRequest.Find(id);
        }

        public IEnumerable<RentalRequest> GetAll()
        {
            return _context.RentalRequest.ToList();
        }

        public void Insert(RentalRequest request)
        {
            request.CreatedDate = DateTime.Now;
            request.UpdatedDate = DateTime.Now;
            _context.RentalRequest.Add(request);
        }

        public void Update(RentalRequest request)
        {
            request.UpdatedDate = DateTime.Now;
            _context.Entry(request).State = EntityState.Modified;
        }

        public void Delete(int Id)
        {
            RentalRequest request = _context.RentalRequest.Find(Id);
            _context.RentalRequest.Remove(request);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void DeleteAll()
        {
            _context.Database.ExecuteSqlRaw("DELETE FROM RentalRequest");
        }

        public void FillTestData()
        {
        }
    }
}
