namespace Installer.Gateway;

public static class GatewayHelper
{
    public static string GetConnectionString(string databaseName)
    {
        var templateUri = Environment.GetEnvironmentVariable("DB_URI")!;
        return templateUri.Replace("<DATABASE_NAME>", databaseName);
    }
}