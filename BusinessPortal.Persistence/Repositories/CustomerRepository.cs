using BusinessPortal.Application.Interface.Persistence;
using BusinessPortal.Domain.Entities;
using BusinessPortal.Persistence.Contexts;
using Dapper;
using System.Data;

namespace BusinessPortal.Persistence.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DapperContext _applicationContext;
        public CustomerRepository(DapperContext applicationContext)
        {
            _applicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));
        }

        #region Queries
        /*Queries*/
        public async Task<int> CountAsync()
        {
            using var connection = _applicationContext.CreateConnection();
            var query = "SELECT COUNT(*) FROM Customer";

            var count = await connection.ExecuteScalarAsync<int>(query, commandType: CommandType.Text);
            return count;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            using var connection = _applicationContext.CreateConnection();
            var query = "CALL Customers_GetAll();";

            var customers = await connection.QueryAsync<Customer>(query, commandType: CommandType.Text);
            return customers;
        }

        public async Task<IEnumerable<Customer>> GetAllWithPaginationAsync(int pageNumber, int pageSize)
        {
            using var connection = _applicationContext.CreateConnection();
            var query = @"
                SELECT * FROM Customer
                LIMIT @PageSize OFFSET @Offset";

            var parameters = new DynamicParameters();
            parameters.Add("PageSize", pageSize);
            parameters.Add("Offset", (pageNumber - 1) * pageSize);

            var customers = await connection.QueryAsync<Customer>(query, param: parameters, commandType: CommandType.Text);
            return customers;
        }

        public async Task<Customer> GetAsync(string id)
        {
            using var connection = _applicationContext.CreateConnection();
            var query = "CALL Customers_GetById(@CustomerID);";

            var parameters = new DynamicParameters();
            parameters.Add("CustomerID", id);

            var customer = await connection.QuerySingleOrDefaultAsync<Customer>(query, param: parameters, commandType: CommandType.Text);
            return customer;
        }
        #endregion

        #region Commands
        /*Commands*/

        public async Task<bool> InsertAsync(Customer entity)
        {
            using var connection = _applicationContext.CreateConnection();
            var query = "CALL Customers_Insert(@CustomerID, @FullName, @Email, @Phone, @Address, @City, @Region, @PostalCode, @Country, @Role);";

            var parameters = new DynamicParameters();
            parameters.Add("CustomerID", entity.CustomerId);
            parameters.Add("FullName", entity.FullName);
            parameters.Add("Email", entity.Email);
            parameters.Add("Role", entity.Role);
            parameters.Add("Address", entity.Address);
            parameters.Add("City", entity.City);
            parameters.Add("Region", entity.Region);
            parameters.Add("PostalCode", entity.PostalCode);
            parameters.Add("Country", entity.Country);
            parameters.Add("Phone", entity.Phone);

            var recordsAffected = await connection.ExecuteAsync(query, param: parameters, commandType: CommandType.Text);
            return recordsAffected > 0;
        }

        public async Task<bool> UpdateAsync(Customer entity)
        {
            using var connection = _applicationContext.CreateConnection();
            var query = "CALL Customers_Update(@CustomerID, @FullName, @Email, @Phone, @Address, @City, @Region, @PostalCode, @Country, @Role);";

            var parameters = new DynamicParameters();
            parameters.Add("CustomerID", entity.CustomerId);
            parameters.Add("FullName", entity.FullName);
            parameters.Add("Email", entity.Email);
            parameters.Add("Role", entity.Role);
            parameters.Add("Address", entity.Address);
            parameters.Add("City", entity.City);
            parameters.Add("Region", entity.Region);
            parameters.Add("PostalCode", entity.PostalCode);
            parameters.Add("Country", entity.Country);
            parameters.Add("Phone", entity.Phone);

            var recordsAffected = await connection.ExecuteAsync(query, param: parameters, commandType: CommandType.Text);
            return recordsAffected > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            using var connection = _applicationContext.CreateConnection();
            var query = "CALL Customers_Delete(@CustomerID);";

            var parameters = new DynamicParameters();
            parameters.Add("CustomerID", id);

            var recordsAffected = await connection.ExecuteAsync(query, param: parameters, commandType: CommandType.Text);
            return recordsAffected > 0;
        }
        #endregion
    }
}
