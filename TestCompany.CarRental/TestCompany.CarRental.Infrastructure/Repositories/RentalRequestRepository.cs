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
    public class RentalRequestRepository : BaseRepository<RentRequest>
    {
        private CarRentalContext _context;

        public RentalRequestRepository(CarRentalContext context) : base(context)
        {
            _context = context;
        }

       
        public override void Insert(RentRequest request)
        {
            request.CreatedDate = DateTime.Now;
            request.UpdatedDate = DateTime.Now;
            base.Insert(request);
        }

        public override void Update(RentRequest request)
        {
            request.UpdatedDate = DateTime.Now;
            base.Update(request);
        }

        public void DeleteAll()
        {
            _context.Database.ExecuteSqlRaw("DELETE FROM RentalRequest");
        }

    }
}
