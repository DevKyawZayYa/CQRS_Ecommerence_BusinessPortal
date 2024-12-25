using BusinessPortal.Application.Interface.Persistence;
using BusinessPortal.Persistence.Contexts;

namespace BusinessPortal.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DapperContext _context;
        private readonly Dictionary<Type, object> _readRepositories = new();
        private readonly Dictionary<Type, object> _writeRepositories = new();

        public UnitOfWork(DapperContext context)
        {
            _context = context;
        }

        public IGenericReadRepository<T> GetReadRepository<T>() where T : class
        {
            if (_readRepositories.TryGetValue(typeof(T), out var repository))
            {
                return (IGenericReadRepository<T>)repository;
            }

            var newRepository = new GenericReadRepository<T>(_context);
            _readRepositories[typeof(T)] = newRepository;
            return newRepository;
        }

        public IGenericWriteRepository<T> GetWriteRepository<T>() where T : class
        {
            if (_writeRepositories.TryGetValue(typeof(T), out var repository))
            {
                return (IGenericWriteRepository<T>)repository;
            }

            var newRepository = new GenericWriteRepository<T>(_context);
            _writeRepositories[typeof(T)] = newRepository;
            return newRepository;
        }

        public async Task<int> CompleteAsync()
        {
            // Implement transaction management if needed
            return await Task.FromResult(0);
        }

        public void Dispose()
        {
            // Dispose resources if needed
        }
    }

}
