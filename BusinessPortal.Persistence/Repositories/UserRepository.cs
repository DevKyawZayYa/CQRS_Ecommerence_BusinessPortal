using BusinessPortal.Application.Interface.Persistence;
using BusinessPortal.Domain.Entities;
using BusinessPortal.Persistence.Contexts;
using Dapper;
using System.Data;

namespace BusinessPortal.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _applicationContext;
        public UserRepository(DapperContext applicationContext)
        {
            _applicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));
        }

        public Task<User> GetAync(string client)
        {
            using var connection = _applicationContext.CreateConnection();
            var query = "UsersGetByClient";

            var parameters = new DynamicParameters();
            parameters.Add("Client", client);

            var user = connection.QuerySingleOrDefaultAsync<User>(query, param: parameters, commandType: CommandType.StoredProcedure);
            return user;
        }
        public async Task<bool> InsertAync(User user)
        {
            using var connection = _applicationContext.CreateConnection();
            var query = "UsersInsert";
            var parameters = new DynamicParameters();
            parameters.Add("UserId", user.UserId);
            parameters.Add("Company", user.Company);
            parameters.Add("Abbreviation", user.Abbreviation);
            parameters.Add("Client", user.Client);
            parameters.Add("Secret", user.Secret);

            var recordsAffected = await connection.ExecuteAsync(query, param: parameters, commandType: CommandType.StoredProcedure);
            return recordsAffected > 0;
        }
    }
}
