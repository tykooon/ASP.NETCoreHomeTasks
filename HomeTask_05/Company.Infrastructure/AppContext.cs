using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

using System.Reflection;

namespace Company.Infrastructure;

public class AppContext : DbContext
{
    private string _dbSourse = string.Empty;
    private bool _enableDbLogging = false;

    public AppContext(string dbSource, bool enableDbLogging = false)
    {
        SQLitePCL.Batteries.Init();
        _dbSourse = dbSource;
        _enableDbLogging = enableDbLogging;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(_dbSourse);
        if (_enableDbLogging)
        {
            optionsBuilder
                .UseLoggerFactory(LoggerFactory.Create(builder => { builder.AddConsole(); }))
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging();
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

}