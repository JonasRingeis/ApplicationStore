using Microsoft.Data.SqlClient;

namespace Installer.Gateway;

public class SqlConnectionFactory
{
    private readonly string _connectionString;
    
    public SqlConnectionFactory(string databaseName, string? connectionString = null)
    {
        _connectionString = connectionString ?? GatewayHelper.GetConnectionString(databaseName);
    }

    public async Task<SqlConnection> CreateConnectionAsync(CancellationToken token = default)
    {
        var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(token);
        return connection;
    }
}