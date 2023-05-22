using System.Data.SQLite;

namespace Company.SqliteDb;

public class DbProvider : IDisposable
{
    private DbProviderOptions _options;
    private SQLiteConnection _connection;

    public DbProviderOptions Options => _options;

    public DbProvider(DbProviderOptions options)
    {
        _options = options;
        _connection = new SQLiteConnection(_options.ConnectionString);
        _connection.Open();
    }

    public DbProvider(string optionsFile) : this(DbProviderOptions.GetFromFile(optionsFile))
    { }

    public void CreateTables(string createScript = null)
    {
        createScript ??= _options.CreateTablesSql;
        using var cmd = new SQLiteCommand(createScript, _connection);
        cmd.ExecuteNonQuery();    // TODO Output to Log
    }

    public void PoputateDb(string insertScript = null)
    {
        insertScript ??= _options.FillTablesSql;
        using var cmd = new SQLiteCommand(insertScript, _connection);
        cmd.ExecuteNonQuery();    // TODO Output to Log
    }

    public string PrepareDb()
    {
        CreateTables();
        PoputateDb();
        return _options.ConnectionString;
    }

    public void Dispose() => _connection.Dispose();
}
