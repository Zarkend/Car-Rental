using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace TestCompany.CarRental.Infrastructure.Repositories
{
    public interface IRepository<T>
    {
        T GetById(int id);
        T[] GetAll();
        IQueryable<T> Query(Expression<Func<T, bool>> filter);
    }
}
