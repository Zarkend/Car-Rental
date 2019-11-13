using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TestCompany.CarRental.Domain.InfrastructureContracts
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task FillTestDataAsync();
        void Delete(TEntity entityToDelete);
        Task DeleteAsync(object id);
        Task<List<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");
        Task<TEntity> GetByIDAsync(object id);
        IEnumerable<TEntity> GetWithRawSql(string query,
            params object[] parameters);
        Task InsertAsync(TEntity entity);
        void Update(TEntity entityToUpdate);

    }
}
