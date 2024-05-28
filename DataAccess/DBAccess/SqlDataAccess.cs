using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace DataAccess.DbAccess;

public class SqlDataAccess : ISqlDataAccess
{
    private readonly IConfiguration _config;

    public SqlDataAccess(IConfiguration config)
    {
        _config = config;
    }

    public async Task<IEnumerable<T>> LoadData<T, U>(
            string psgFun,
            U parameters,
            string connectionId = "Default")
    {
        var sqlQuery = $"select * from {psgFun}";

        using IDbConnection connection = new Npgsql.NpgsqlConnection(_config.GetConnectionString(connectionId));
        return await connection.QueryAsync<T>(sqlQuery, parameters);
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
