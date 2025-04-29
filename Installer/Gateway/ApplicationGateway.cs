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
        GatewayHelper.UseColumnAttributeNameMapping<InstallationData>();
    }

    public async Task<Application[]> GetAllApplications()
    {
        await using var connection = await _sqlConnectionFactory.CreateConnectionAsync();
        
        return (await connection.QueryAsync<Application>("""
                                                              SELECT A.*, P.publisher_name FROM dbo.applications AS A
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

    public async Task<InstallationData> GetInstallationData(int versionId)
    {
        await using var connection = await _sqlConnectionFactory.CreateConnectionAsync();

        return await connection.QueryFirstAsync<InstallationData>("""
                                                                  SELECT A.download_url, A.checksum_hash,
                                                                         C.algorithm_name AS checksum_algorithm
                                                                  FROM dbo.application_versions as A
                                                                  JOIN dbo.checksum_algorithms AS C ON C.algorithm_id = A.checksum_algorithm_id
                                                                  WHERE A.application_version_id = @versionId;
                                                                  """, new { versionId });
    }
}