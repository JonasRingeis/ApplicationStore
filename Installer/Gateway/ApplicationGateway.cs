using Dapper;
using Installer.Model;

namespace Installer.Gateway;

public class ApplicationGateway
{
    private readonly SqlConnectionFactory _sqlConnectionFactory;
        
    public ApplicationGateway()
    {
        _sqlConnectionFactory = new SqlConnectionFactory("app_store");
    }

    public async Task<Application[]> GetAllApplications()
    {
        await using var connection = await _sqlConnectionFactory.CreateConnectionAsync();

        var result = await connection.QueryAsync("""
             SELECT a.*, P.publisher_name FROM dbo.applications AS A
             JOIN dbo.publishers AS P ON P.publisher_id = A.publisher_id;
        """);
        
        return result.Select(row =>
            new Application
            {
                ApplicationId = row.application_id,
                Title = row.title,
                Description = row.description,
                PublisherId = row.publisher_id,
                PublisherName = row.publisher_name
            }
        ).ToArray();
    } 
}