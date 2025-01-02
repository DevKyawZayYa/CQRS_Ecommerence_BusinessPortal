using BusinessPortal.Application.Interface.Persistence;
using BusinessPortal.Persistence.Contexts;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
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
            var query = $"CALL {typeof(T).Name}s_GetAllWithPagination(@PageNumber, @PageSize)";
            var parameters = new DynamicParameters();
            parameters.Add("PageNumber", pageNumber);
            parameters.Add("PageSize", pageSize);
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


        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            using var connection = _context.CreateConnection();

            // Convert the predicate to a SQL WHERE clause and parameters
            var queryBuilder = new StringBuilder($"SELECT * FROM {typeof(T).Name} WHERE ");
            var parameters = new DynamicParameters();

            var body = (BinaryExpression)predicate.Body;
            var left = (MemberExpression)body.Left;
            var right = body.Right as ConstantExpression;

            if (right == null)
            {
                var member = body.Right as MemberExpression;
                if (member != null)
                {
                    right = Expression.Constant(Expression.Lambda<Func<object>>(Expression.Convert(member, typeof(object))).Compile()());
                }
            }

            queryBuilder.Append($"{left.Member.Name} = @{left.Member.Name}");
            parameters.Add($"@{left.Member.Name}", right.Value);

            queryBuilder.Append(" LIMIT 1");

            var query = queryBuilder.ToString();

            return await connection.QueryFirstOrDefaultAsync<T>(query, parameters);
        }


    }
}
