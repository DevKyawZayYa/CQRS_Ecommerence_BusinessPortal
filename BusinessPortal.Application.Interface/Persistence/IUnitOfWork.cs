using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessPortal.Application.Interface.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        ICustomerRepository Customers { get; }
    }
}
