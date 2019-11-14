using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TestCompany.CarRental.Domain.InfrastructureContracts;
using TestCompany.CarRental.Infrastructure.DbContexts;

namespace TestCompany.CarRental.Infrastructure.Repositories
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        internal CarRentalContext context;
        internal DbSet<TEntity> dbSet;

        public BaseRepository(CarRentalContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> GetWithRawSql(string query,
            params object[] parameters)
        {
            return dbSet.FromSqlRaw(query, parameters);
        }

        public virtual async Task<List<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }


            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public async virtual Task<TEntity> GetByIDAsync(object id)
        {
            return await dbSet.FindAsync(id);
        }

        public async virtual Task InsertAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
        }

        public async virtual Task DeleteAsync(object id)
        {
            TEntity entityToDelete = await dbSet.FindAsync(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public async virtual Task FillTestDataAsync()
        {
            throw new NotImplementedException();
        }

        public TEntity Entry(TEntity entry)
        {
           return context.Entry(entry).Entity;
        }
    }
}
