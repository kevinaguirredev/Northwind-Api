using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindApi.IRepository
{
    public interface IUnitOfWork : IDisposable
    {

        IGenericRepository<Person> Persons { get; }

        Task Save();

    }
}
