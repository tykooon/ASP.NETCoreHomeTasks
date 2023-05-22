using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

using System.Reflection;

namespace Company.Infrastructure;

public class AppContext : DbContext
{
    private const string ConnectionString = "DataSource=candidates.db";

    public AppContext()
    {
        SQLitePCL.Batteries.Init();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlite(ConnectionString)
            .UseLoggerFactory(LoggerFactory.Create(builder => { builder.AddConsole(); }))
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

}