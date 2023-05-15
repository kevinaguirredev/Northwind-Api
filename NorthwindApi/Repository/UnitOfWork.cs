using NorthwindApi.IRepository;
using NorthwindApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindApi.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly NorthwindContext northwindContext;

        private IGenericRepository<Person> persons;

        public UnitOfWork(NorthwindContext dbContext)
        {
            this.northwindContext = dbContext;
        }

        public IGenericRepository<Person> Persons => persons ??= new GenericRepository<Person>(northwindContext);

        public void Dispose()
        {
            northwindContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await northwindContext.SaveChangesAsync();
        }

    }

}
