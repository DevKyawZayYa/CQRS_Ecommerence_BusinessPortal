using BusinessPortal.Application.Interface.Persistence;
using BusinessPortal.Persistence.Contexts;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BusinessPortal.Persistence.Repositories
{
    public class GenericWriteRepository<T> : IGenericWriteRepository<T> where T : class
    {
        private readonly DapperContext _context;

        public GenericWriteRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<bool> InsertAsync(T entity)
        {
            using var connection = _context.CreateConnection();
            var query = $"CALL {typeof(T).Name}s_Insert({GetParameterList(entity)})";
            var parameters = new DynamicParameters(entity);
            var recordsAffected = await connection.ExecuteAsync(query, param: parameters, commandType: CommandType.Text);
            return recordsAffected > 0;
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            using var connection = _context.CreateConnection();
            var query = $"CALL {typeof(T).Name}s_Update({GetParameterList(entity)})";
            var parameters = new DynamicParameters(entity);
            var recordsAffected = await connection.ExecuteAsync(query, param: parameters, commandType: CommandType.Text);
            return recordsAffected > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            using var connection = _context.CreateConnection();
            var query = $"CALL {typeof(T).Name}s_Delete(@Id)";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id);
            var recordsAffected = await connection.ExecuteAsync(query, param: parameters, commandType: CommandType.Text);
            return recordsAffected > 0;
        }

        private string GetParameterList(T entity)
        {
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var parameterList = properties.Select(p => $"@{p.Name}").ToArray();
            return string.Join(", ", parameterList);
        }
    }
}


