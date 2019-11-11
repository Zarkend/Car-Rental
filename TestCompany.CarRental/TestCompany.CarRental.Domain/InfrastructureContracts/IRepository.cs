using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace TestCompany.CarRental.Domain.InfrastructureContracts
{
    public interface IRepository<T>
    {
        void FillTestData();
        T GetById(int id);
        IEnumerable<T> GetAll();
        IQueryable<T> Query(Expression<Func<T, bool>> filter);
        void Insert(T item);
        void Update(T item);
        void Delete(int Id);
        void Save();

    }
}
