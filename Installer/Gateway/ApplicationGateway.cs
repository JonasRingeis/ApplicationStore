using System.ComponentModel.DataAnnotations.Schema;
using Dapper;
using Installer.Model;
using Microsoft.IdentityModel.Tokens;

namespace Installer.Gateway;

public class ApplicationGateway : IApplicationModel
{
    private readonly SqlConnectionFactory _sqlConnectionFactory;
        
    public ApplicationGateway()
    {
        _sqlConnectionFactory = new SqlConnectionFactory("app_store");
        GatewayHelper.UseStripUnderscoreMapping<Application>();
        // GatewayHelper.UseColumnAttributeNameMapping<Application>();
    }

    public async Task<Application[]> GetAllApplications()
    {
        await using var connection = await _sqlConnectionFactory.CreateConnectionAsync();
        
        return (await connection.QueryAsync<Application>("""
                                                              SELECT a.*, P.publisher_name FROM dbo.applications AS A
                                                              JOIN dbo.publishers AS P ON P.publisher_id = A.publisher_id;
                                                         """)).ToArray();
    }
}