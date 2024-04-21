using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace DataAccess.DBAccess;

public class SqlDataAccess : ISqlDataAccess
{
    private readonly IConfiguration _config;

    public SqlDataAccess(IConfiguration config)
    {
        _config = config;
    }

    public async Task<IEnumerable<T>> LoadData<T, U>(
            string storeProcedure,
            U parameters,
            string connectionId = "Default")
    {
        using IDbConnection connection = new Npgsql.NpgsqlConnection(_config.GetConnectionString(connectionId));
        return await connection.QueryAsync<T>(storeProcedure, parameters, commandType: CommandType.StoredProcedure);
    }

    public async Task SaveData<T>(
            string storeProcedure,
            T parameters,
            string connectionId = "Default")
    {
        using IDbConnection connection = new Npgsql.NpgsqlConnection(_config.GetConnectionString(connectionId));
        await connection.ExecuteAsync(storeProcedure, parameters, commandType: CommandType.StoredProcedure);
    }
}
