using BusinessPortal.Application.Interface.Persistence;
using BusinessPortal.Persistence.Contexts;
using BusinessPortal.Persistence.Repositories;
using Dapper;
using System.Data;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly DapperContext _context;
    private IDbTransaction? _transaction;
    private IDbConnection? _connection;

    public UnitOfWork(DapperContext context)
    {
        _context = context;
        _connection = _context.CreateConnection();
        _connection.Open();
        _transaction = _connection.BeginTransaction();
    }

    public IGenericWriteRepository<T> GetWriteRepository<T>() where T : class
    {
        return new GenericWriteRepository<T>(_context);
    }

    public IGenericReadRepository<T> GetReadRepository<T>() where T : class
    {
        return new GenericReadRepository<T>(_context);
    }

    public async Task<int> CompleteAsync()
    {
        if (_transaction == null)
        {
            throw new InvalidOperationException("Transaction has not been initialized.");
        }

        try
        {
            var result = await _transaction.Connection!.ExecuteAsync("COMMIT");
            return result;
        }
        catch
        {
            await _transaction.Connection!.ExecuteAsync("ROLLBACK");
            throw;
        }
        finally
        {
            _transaction?.Dispose();
            _connection?.Close();
            _transaction = null;
            _connection = null;
        }
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _connection?.Dispose();
    }
}
