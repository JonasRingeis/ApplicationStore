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
    
    public static void UseColumnAttributeNameMapping<T>(bool caseSensitive = false)
    {
        var comparisonMethod = caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
        SqlMapper.SetTypeMap(typeof(T), new CustomPropertyTypeMap(typeof(T), (type, columnName) => 
            type.GetProperties().FirstOrDefault(prop =>
                prop.GetCustomAttributes(false).OfType<ColumnAttribute>()
                    .Select(attr => attr.Name!)
                    .FirstOrDefault(prop.Name)
                    .Equals(columnName, comparisonMethod)
            ) ?? throw new InvalidOperationException("The given type has no properties.")
        ));
    }

    public static void UseStripUnderscoreMapping<T>(bool caseSensitive = false)
    {
        var comparisonMethod = caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
        SqlMapper.SetTypeMap(typeof(T), new CustomPropertyTypeMap(typeof(T), (type, columnName) =>
            {
                var strippedColumnName = columnName.Replace("_", string.Empty);
                return type.GetProperties().FirstOrDefault(prop =>
                    prop.Name.Replace("_", string.Empty).Equals(strippedColumnName, comparisonMethod)
                ) ?? throw new InvalidOperationException("The given type has no properties.");
            }
        ));
    }
}