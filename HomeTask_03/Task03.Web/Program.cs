using Company.Core.Interfaces;
using Company.Infrastructure;
using Company.SqliteDb;


using (var db = new DbProvider("CompanyDbSqlite.yaml"))
{
    db.PrepareDb();
}

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();
app.Run();
