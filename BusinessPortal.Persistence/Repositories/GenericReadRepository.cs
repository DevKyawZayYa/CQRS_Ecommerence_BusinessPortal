using BusinessPortal.Application.Interface.Persistence;
using BusinessPortal.Persistence.Contexts;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessPortal.Persistence.Repositories
{
    public class GenericReadRepository<T> : IGenericReadRepository<T> where T : class
    {
        private readonly DapperContext _context;

        public GenericReadRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> CountAsync()
        {
            using var connection = _context.CreateConnection();
            var query = $"CALL {typeof(T).Name}s_Count()";
            return await connection.ExecuteScalarAsync<int>(query, commandType: CommandType.Text);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            using var connection = _context.CreateConnection();
            var query = $"CALL {typeof(T).Name}s_GetAll()";
            return await connection.QueryAsync<T>(query, commandType: CommandType.Text);
        }

        public async Task<IEnumerable<T>> GetAllWithPaginationAsync(int pageNumber, int pageSize)
        {
            using var connection = _context.CreateConnection();
            var query = $"CALL {typeof(T).Name}s_GetAllWithPagination(@PageSize, @Offset)";
            var parameters = new DynamicParameters();
            parameters.Add("PageSize", pageSize);
            parameters.Add("Offset", (pageNumber - 1) * pageSize);
            return await connection.QueryAsync<T>(query, param: parameters, commandType: CommandType.Text);
        }

        public async Task<T> GetAsync(Guid id)
        {
            using var connection = _context.CreateConnection();
            var query = $"CALL {typeof(T).Name}s_GetById(@Id)";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id);
            return await connection.QuerySingleOrDefaultAsync<T>(query, param: parameters, commandType: CommandType.Text);
        }
    }
}
