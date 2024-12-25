using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessPortal.Application.Interface.Persistence
{
    public interface IGenericRepository<T> where T : class
    {
        /* Commands */
        Task<bool> InsertAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(string id);
        /* Queries */
        Task<T> GetAsync(string id);
        Task<IEnumerable<T>> GetAllAsync();
    }
}
