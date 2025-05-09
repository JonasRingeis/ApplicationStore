using Microsoft.Data.SqlClient;

namespace Installer.Gateway;

public class SqlConnectionFactory(string databaseName, string? connectionString = null)
{
    private readonly string _connectionString = connectionString ?? GatewayHelper.GetConnectionString(databaseName);

    /// <summary>
    /// Creates a new sql connection to the database specified in the constructor.
    /// </summary>
    /// <param name="token">Cancellation Token for Connection</param>
    /// <returns>The newly created SqlConnection</returns>
    public async Task<SqlConnection> CreateConnectionAsync(CancellationToken token = default)
    {
        var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(token);
        return connection;
    }
}