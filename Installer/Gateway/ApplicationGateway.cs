using System.ComponentModel.DataAnnotations.Schema;
using Dapper;
using Installer.Model;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;

namespace Installer.Gateway;

public class ApplicationGateway : IApplicationModel
{
    private readonly SqlConnectionFactory _sqlConnectionFactory;
        
    public ApplicationGateway()
    {
        _sqlConnectionFactory = new SqlConnectionFactory("app_store");

        GatewayHelper.UseColumnAttributeNameMapping<Application>();
        GatewayHelper.UseColumnAttributeNameMapping<ApplicationVersion>();
    }

    public async Task<Application[]> GetAllApplications()
    {
        await using var connection = await _sqlConnectionFactory.CreateConnectionAsync();
        
        return (await connection.QueryAsync<Application>("""
                                                              SELECT a.*, P.publisher_name FROM dbo.applications AS A
                                                              JOIN dbo.publishers AS P ON P.publisher_id = A.publisher_id;
                                                         """)).ToArray();
    }

    public async Task<ApplicationVersion[]> GetApplicationVersions(int appId)
    {
        await using var connection = await _sqlConnectionFactory.CreateConnectionAsync();
        
        return (await connection.QueryAsync<ApplicationVersion>("""
                                                                SELECT application_version_id, version_name, uploaded_at from dbo.application_versions
                                                                WHERE application_id = @appId
                                                                ORDER BY uploaded_at DESC;
                                                                """, new { appId })).ToArray();
    }
}