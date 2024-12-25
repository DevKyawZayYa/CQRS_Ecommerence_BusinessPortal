using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessPortal.Application.Interface.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericReadRepository<T> GetReadRepository<T>() where T : class;
        IGenericWriteRepository<T> GetWriteRepository<T>() where T : class;
        Task<int> CompleteAsync();
    }

}
