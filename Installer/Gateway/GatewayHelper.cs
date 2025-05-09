using System.ComponentModel.DataAnnotations.Schema;
using Dapper;

namespace Installer.Gateway;

public static class GatewayHelper
{
    /// <summary>
    /// Builds a database connection string with the base coming from an env var called DB_URI.
    /// The env var should contain a placeholder for the db name like this: '&lt;DATABASE_NAME&gt;'.
    /// </summary>
    /// <param name="databaseName">The database name used for the connection string</param>
    /// <returns>The build connection string</returns>
    /// <exception cref="Exception">Env var 'DB_URI' is not present</exception>
    public static string GetConnectionString(string databaseName)
    {
        var templateUri = Environment.GetEnvironmentVariable("DB_URI") ?? throw new Exception("DB_URI is not set in environment variables.");
        return templateUri.Replace("<DATABASE_NAME>", databaseName);
    }
    
    /// <summary>
    /// Created a new sql mapper that reads the <c>[Column]</c> attributes from properties of given entity type.
    /// If no <c>[Column]</c> attribute is set, the property name will be used. 
    /// </summary>
    /// <param name="caseSensitive">If <c>true</c> matches column names case-sensitive</param>
    /// <typeparam name="T">Entity type to map</typeparam>
    /// <exception cref="InvalidOperationException">Given Entity has no properties</exception>
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

    /// <summary>
    /// Created a new sql mapper that compares the property names with entity column names and strips any underscores.
    /// </summary>
    /// <param name="caseSensitive">If <c>true</c> matches column names case-sensitive</param>
    /// <typeparam name="T">Entity type to map</typeparam>
    /// <exception cref="InvalidOperationException">Given Entity has no properties</exception>
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