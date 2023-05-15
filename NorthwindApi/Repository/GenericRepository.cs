using Microsoft.EntityFrameworkCore;
using NorthwindApi.IRepository;
using NorthwindApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NorthwindApi.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        private readonly NorthwindContext northwindContext;
        private readonly DbSet<T> northwindDbSet;

        public GenericRepository(NorthwindContext northwindContext)
        {
            this.northwindContext = northwindContext;
            northwindDbSet = this.northwindContext.Set<T>();
        }

        public async Task Delete(int id)
        {
            var entity = await northwindDbSet.FindAsync(id);
            northwindDbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            northwindDbSet.RemoveRange(entities);
        }

        public async Task<T> Get(Expression<Func<T, bool>> expression, List<string> includes = null)
        {
            IQueryable<T> query = northwindDbSet;

            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.AsNoTracking().FirstOrDefaultAsync(expression);

        }

        public async Task<IList<T>> GetAll(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<string> includes = null)
        {
            IQueryable<T> query = northwindDbSet;

            if (expression != null)
            {
                query = query.Where(expression);
            }

            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task Insert(T entity)
        {
            await northwindDbSet.AddAsync(entity);
        }

        public async Task InsertRange(IEnumerable<T> entities)
        {
            await northwindDbSet.AddRangeAsync(entities);
        }

        public void Update(T entity)
        {
            northwindDbSet.Attach(entity);
            northwindContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
