using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Company.SqliteDb;

public class DbProviderOptions
{
    public string ConnectionString { get; set; } = "DataSource=default.db";
    public string CreateTablesSql { get; set; } = "";
    public string FillTablesSql { get; set; } = "";

    public static DbProviderOptions GetFromFile(string filename)
    {
        if (!File.Exists(filename))
        {
            return new DbProviderOptions();
        }

        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();

        return deserializer.Deserialize<DbProviderOptions>(File.ReadAllText(filename));
    }
}
