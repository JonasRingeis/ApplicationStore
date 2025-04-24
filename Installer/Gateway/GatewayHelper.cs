using System.ComponentModel.DataAnnotations.Schema;
using Dapper;

namespace Installer.Gateway;

public static class GatewayHelper
{
    public static string GetConnectionString(string databaseName)
    {
        var templateUri = Environment.GetEnvironmentVariable("DB_URI")!;
        return templateUri.Replace("<DATABASE_NAME>", databaseName);
    }
    
    public static void SetMapType<T>()
    {
        SqlMapper.SetTypeMap(typeof(T), new CustomPropertyTypeMap(typeof(T), (type, columnName) => 
            type.GetProperties().FirstOrDefault(prop =>
                prop.GetCustomAttributes(false).OfType<ColumnAttribute>()
                    .Select(attr => attr.Name!)
                    .FirstOrDefault(prop.Name)
                    .Equals(columnName, StringComparison.OrdinalIgnoreCase)
            ) ?? throw new InvalidOperationException("The given type has no properties.")
        ));
    }
}